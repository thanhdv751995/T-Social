using Extention.Management.BackgroundJob;
using Extention.Management.EntityFrameworkCore;
using Extention.Management.Hub;
using Extention.Management.Kafka;
using Extention.Management.Logs;
using Extention.Management.Randoms;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Net;

namespace Extention.Management
{
    public class Startup
    {
        IConfiguration configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            configuration = services.GetConfiguration();

            services.AddScoped<ManagementDbContext>();
            services.AddScoped<ILogsAppService, LogAppService>();
            services.AddApplication<ManagementHttpApiHostModule>();

            services.AddSignalR()
                .AddStackExchangeRedis(options =>
                {
                    //options.Configuration.ChannelPrefix = "TSocialSignalR";

                    options.ConnectionFactory = async writer =>
                    {
                        var config = new ConfigurationOptions
                        {
                            AbortOnConnectFail = false
                        };

                        config.EndPoints.Add(configuration["Redis:Server"], int.Parse(configuration["Redis:Port"]));
                        config.AllowAdmin = bool.Parse(configuration["Redis:AllowAdmin"]);
                        config.Password = configuration["Redis:Password"];
                        config.ClientName = configuration["Redis:ClientName"];

                        config.SetDefaultPorts();

                        var connection = await ConnectionMultiplexer.ConnectAsync(config, writer);
                        connection.ConnectionFailed += (_, e) =>
                        {
                            Console.WriteLine("Connection to Redis failed.");
                        };

                        if (!connection.IsConnected)
                        {
                            Console.WriteLine("Did not connect to Redis.");
                        }

                        return connection;
                    };
                });

            services.AddHangfireServer();
        }

#pragma warning disable CA1041 // Provide ObsoleteAttribute message
        [System.Obsolete]
#pragma warning restore CA1041 // Provide ObsoleteAttribute message
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
            IRecurringJobManager recurringJobManager, BackgroundJobAppService backgroundJobAppService, IHubContext<HubSignalR> _hub)
        {
            app.InitializeApplication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<HubSignalR>("/notify");
            });

            app.UseHangfireServer();
            var dashboardOption = new DashboardOptions
            { 
                DashboardTitle = "TSocial",
                Authorization = new[]
                {
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        User = configuration["HangfireSettings:UserName"],
                        Pass = configuration["HangfireSettings:Password"]
                    }
                }
            };
            app.UseHangfireDashboard("/hangfire", dashboardOption);

            backgroundJobAppService.CheckClientDailyJob();

            backgroundJobAppService.InvokeCloseChrome();

            backgroundJobAppService.InvokeNewScriptClientUsingBackgroundJob();

            backgroundJobAppService.CheckExceptionCloseChromeBackgroundJob();
        }
    }
}

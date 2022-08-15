using ActiviteChart.ActiviteChartDatas;
using Confluent.Kafka;
using Extention.Management.KafkaProducer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Extention.Management
{
    public class KafkaConsumerService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly ConsumerConfig conf;
        private readonly ActiviteChartDataAppService _activiteChartDataAppService;
        public KafkaConsumerService(IConfiguration configuration,ActiviteChartDataAppService activiteChartDataAppService)
        {
            _configuration = configuration;
            _activiteChartDataAppService = activiteChartDataAppService;
            conf = new ConsumerConfig
            {
                GroupId = _configuration.GetSection("Kafka")["GroupId"],
                BootstrapServers = _configuration.GetSection("Kafka")["BootstrapServer"],
                AutoOffsetReset = AutoOffsetReset.Earliest,
                MaxPartitionFetchBytes = 734003
            };
        }

        public async void Consumer()
        {
            using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
            c.Subscribe(_configuration.GetSection("Kafka")["Topic"]);

            CancellationTokenSource cts = new();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };

            try
            {
                while (true)
                {
                    try
                    {
                        var cr = c.Consume(cts.Token);
                        var createActivityClient = JsonConvert.DeserializeObject<CreateActiviteChartDataDto>(cr.Message.Value);
                        var entity=await  _activiteChartDataAppService.CreateAsync(createActivityClient);
                        Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occured: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Ensure the consumer leaves the group cleanly and final offsets are committed.
                c.Close();
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Consumer();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace ActiviteChart.MongoDB
{
    [DependsOn(
        typeof(ActiviteChartTestBaseModule),
        typeof(ActiviteChartMongoDbModule)
        )]
    public class ActiviteChartMongoDbTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var stringArray = ActiviteChartMongoDbFixture.ConnectionString.Split('?');
                        var connectionString = stringArray[0].EnsureEndsWith('/')  +
                                                   "Db_" +
                                               Guid.NewGuid().ToString("N") + "/?" + stringArray[1];

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connectionString;
            });
        }
    }
}

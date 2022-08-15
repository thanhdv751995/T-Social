using ActiviteChart.MongoDB;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace ActiviteChart.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(ActiviteChartMongoDbModule),
        typeof(ActiviteChartApplicationContractsModule)
        )]
    public class ActiviteChartDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}

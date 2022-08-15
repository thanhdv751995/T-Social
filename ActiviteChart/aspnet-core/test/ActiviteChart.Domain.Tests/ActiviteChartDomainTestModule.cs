using ActiviteChart.MongoDB;
using Volo.Abp.Modularity;

namespace ActiviteChart
{
    [DependsOn(
        typeof(ActiviteChartMongoDbTestModule)
        )]
    public class ActiviteChartDomainTestModule : AbpModule
    {

    }
}
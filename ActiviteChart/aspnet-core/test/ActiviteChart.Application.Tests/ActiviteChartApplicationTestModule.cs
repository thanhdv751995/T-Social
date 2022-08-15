using Volo.Abp.Modularity;

namespace ActiviteChart
{
    [DependsOn(
        typeof(ActiviteChartApplicationModule),
        typeof(ActiviteChartDomainTestModule)
        )]
    public class ActiviteChartApplicationTestModule : AbpModule
    {

    }
}
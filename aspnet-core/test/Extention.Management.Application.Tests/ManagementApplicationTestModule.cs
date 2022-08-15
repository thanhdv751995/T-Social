using Volo.Abp.Modularity;

namespace Extention.Management
{
    [DependsOn(
        typeof(ManagementApplicationModule),
        typeof(ManagementDomainTestModule)
        )]
    public class ManagementApplicationTestModule : AbpModule
    {

    }
}
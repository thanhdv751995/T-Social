using Extention.Management.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Extention.Management
{
    [DependsOn(
        typeof(ManagementEntityFrameworkCoreTestModule)
        )]
    public class ManagementDomainTestModule : AbpModule
    {

    }
}
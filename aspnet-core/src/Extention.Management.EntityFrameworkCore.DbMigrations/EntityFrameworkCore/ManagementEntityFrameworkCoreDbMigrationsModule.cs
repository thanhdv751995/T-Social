using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Extention.Management.EntityFrameworkCore
{
    [DependsOn(
        typeof(ManagementEntityFrameworkCoreModule)
        )]
    public class ManagementEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ManagementMigrationsDbContext>();
        }
    }
}

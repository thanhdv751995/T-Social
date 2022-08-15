using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Extention.Management.Data;
using Volo.Abp.DependencyInjection;

namespace Extention.Management.EntityFrameworkCore
{
    public class EntityFrameworkCoreManagementDbSchemaMigrator
        : IManagementDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreManagementDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the ManagementMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<ManagementMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
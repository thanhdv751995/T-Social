using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Extention.Management.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class ManagementMigrationsDbContextFactory : IDesignTimeDbContextFactory<ManagementMigrationsDbContext>
    {
        public ManagementMigrationsDbContext CreateDbContext(string[] args)
        {
            ManagementEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<ManagementMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new ManagementMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Extention.Management.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}

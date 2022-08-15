using System.Threading.Tasks;

namespace Extention.Management.Data
{
    public interface IManagementDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}

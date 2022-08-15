using System.Threading.Tasks;

namespace ActiviteChart.Data
{
    public interface IActiviteChartDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}

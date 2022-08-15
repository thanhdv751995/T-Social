using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ActiviteChart.Data
{
    /* This is used if database provider does't define
     * IActiviteChartDbSchemaMigrator implementation.
     */
    public class NullActiviteChartDbSchemaMigrator : IActiviteChartDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}
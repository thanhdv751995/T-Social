using MongoDB.Driver;
using ActiviteChart.Users;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;
using ActiviteChart.ActiviteChartDatas;

namespace ActiviteChart.MongoDB
{
    [ConnectionStringName("Default")]
    public class ActiviteChartMongoDbContext : AbpMongoDbContext
    {
        public IMongoCollection<ActiviteChartData> Users => Collection<ActiviteChartData>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.Entity<ActiviteChartData>(b =>
            {
                /* Sharing the same "AbpUsers" collection
                 * with the Identity module's IdentityUser class. */
                b.CollectionName = "ActiviteChart";
            });
        }
    }
}

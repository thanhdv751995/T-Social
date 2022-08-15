using ActiviteChart.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace ActiviteChart.ActiviteChartDatas
{
    public class ActiviteChartDataRepository : MongoDbRepository<ActiviteChartMongoDbContext, ActiviteChartData, Guid>, IActiviteChartDataRepository
    {
        public ActiviteChartDataRepository(IMongoDbContextProvider<ActiviteChartMongoDbContext> dbContextProvider)
       : base(dbContextProvider)
        {
        }
    }
}

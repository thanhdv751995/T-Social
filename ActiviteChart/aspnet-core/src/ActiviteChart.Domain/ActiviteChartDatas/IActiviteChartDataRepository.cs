using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ActiviteChart.ActiviteChartDatas
{
    public interface IActiviteChartDataRepository : IRepository<ActiviteChartData, Guid>
    {
    }
}

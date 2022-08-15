using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace ActiviteChart.ActiviteChartDatas
{
    public class ActiviteChartDataManager : DomainService
    {
        public ActiviteChartData CreateAsync(
          [NotNull] string userName,
          [NotNull] Guid scriptId,
          [NotNull] string typeName,
          [NotNull] string content
          )
        {
            return new ActiviteChartData(
                GuidGenerator.Create(),
                userName,
                scriptId,
                typeName,
                content
            );
        }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.Histories
{
    public class HistoryManager : DomainService
    {
        public HistoryManager()
        {

        }

        public History CreateAsync(
            [NotNull] Guid clientId,
            [NotNull] Guid scriptId,
            DateTime? timeStart,
            DateTime? timeEnd
            )
        {
            return new History(
                GuidGenerator.Create(),
                clientId,
                scriptId,
                timeStart,
                timeEnd
            );
        }
    }
}

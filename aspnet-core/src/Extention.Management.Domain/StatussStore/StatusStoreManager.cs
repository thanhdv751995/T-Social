using Extention.Management.EStatusType;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.StatussStore
{
    public class StatusStoreManager : DomainService
    {
        public StatusStore CreateAsync(
           [NotNull] StatusType statusType,
           [NotNull] string content
           )
        {
            return new StatusStore(
                GuidGenerator.Create(),
                statusType,
                content
            );
        }
    }
}

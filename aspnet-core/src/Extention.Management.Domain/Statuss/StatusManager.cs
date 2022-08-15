using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.Statuss
{
    public class StatusManager : DomainService
    {
        public Status CreateAsync(
           [NotNull] Guid idUser,
           [NotNull] string content
           )
        {
            return new Status(
                GuidGenerator.Create(),
                idUser,
                content
            );
        }
    }
}

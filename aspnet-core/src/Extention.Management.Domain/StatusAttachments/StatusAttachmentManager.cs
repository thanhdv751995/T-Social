using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.StatusAttachments
{
    public class StatusAttachmentManager : DomainService
    {
        public StatusAttachment CreateAsync(
           [NotNull] Guid idStatus,
           [NotNull] string url
           )
        {
            return new StatusAttachment(
                GuidGenerator.Create(),
                idStatus,
                url
            );
        }
    }
}

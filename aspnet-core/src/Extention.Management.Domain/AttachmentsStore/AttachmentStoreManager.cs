using Extention.Management.EStatusType;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.AttachmentsStore
{
    public class AttachmentStoreManager : DomainService
    {
        public AttachmentStoreManager()
        {

        }

        public AttachmentsStore CreateAsync(
            [NotNull] StatusType statusType,
            [NotNull] string url
            )
        {
            return new AttachmentsStore(
                GuidGenerator.Create(),
                statusType,
                url
            );
        }
    }
}

using Extention.Management.EStatusType;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.AttachmentsStore
{
    public class AttachmentsStore : AuditedAggregateRoot<Guid>
    {
        public StatusType StatusType { get; set; }
        public string URL { get; set; }
        private AttachmentsStore()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal AttachmentsStore(
               Guid id,
               [NotNull] StatusType statusType,
               [NotNull] string url
           )
           : base(id)
        {
            StatusType = statusType;
            URL = url;
        }
    }
}

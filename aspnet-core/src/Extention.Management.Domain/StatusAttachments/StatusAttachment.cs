using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.StatusAttachments
{
    public class StatusAttachment : AuditedAggregateRoot<Guid>
    {
        public Guid IdStatus  { get; set; }
        public string URL { get; set; }
        private StatusAttachment()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal StatusAttachment(
               Guid id,
               [NotNull] Guid idStatus,
               [NotNull] string url
           )
           : base(id)
        {
            IdStatus = idStatus;
            URL = url;
        }
    }
}

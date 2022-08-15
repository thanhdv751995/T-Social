using Extention.Management.EStatusType;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.StatussStore
{
    public class StatusStore : AuditedAggregateRoot<Guid>
    {
        public StatusType StatusType { get; set; }
        public string Content { get; set; }
        private StatusStore()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal StatusStore(
               Guid id,
               [NotNull] StatusType statusType,
               string content
           )
           : base(id)
        {
            StatusType = statusType;
            Content = content;
        }
    }
}

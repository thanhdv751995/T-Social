using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.Histories
{
    public class History : AuditedAggregateRoot<Guid>
    {
        public Guid ClientId { get; set; }
        public Guid ScriptId { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        private History()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal History(
               Guid id,
               [NotNull] Guid clientId,
               Guid scriptId,
               DateTime? timeStart,
               DateTime? timeEnd
           )
           : base(id)
        {
            ClientId = clientId;
            ScriptId = scriptId;
            TimeStart = timeStart;
            TimeEnd = timeEnd;
        }
    }
}

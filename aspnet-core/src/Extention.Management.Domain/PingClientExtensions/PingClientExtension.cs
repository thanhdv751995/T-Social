using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.PingClientExtensions
{
    public class PingClientExtension : AuditedAggregateRoot<Guid>
    {
        public Guid ClientId { get; set; }
        public Boolean IsPingSuccess { get; set; }
        private PingClientExtension()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal PingClientExtension(
               Guid id,
               [NotNull] Guid clientId,
                Boolean isPingSuccess
           )
           : base(id)
        {
            ClientId = clientId;
            IsPingSuccess = isPingSuccess;
           
        }
    }
}

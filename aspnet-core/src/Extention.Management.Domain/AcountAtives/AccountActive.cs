using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.AcountAtives
{
    public class AccountActive : AuditedAggregateRoot<Guid>
    {
        public Guid ClientId { get; set; }
        public Guid ProxyId { get; set; }
        private AccountActive()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal AccountActive(
               Guid id,
               [NotNull] Guid clientId,
               [NotNull] Guid proxyId
           )
           : base(id)
        {
            ClientId = clientId;
            ProxyId = proxyId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.ProfileOfClients
{
    public class ClientBelongToProfile: AuditedAggregateRoot<Guid>
    {
        public Guid ProfileClientId { get; set; }
        public Guid ClientId { get; set; }
        public ClientBelongToProfile()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal ClientBelongToProfile(
               Guid id,
               [NotNull] Guid profileClientId,
               [NotNull] Guid clientId
           )
           : base(id)
        {
            ProfileClientId = profileClientId;
            ClientId = clientId;
        }
    }
}

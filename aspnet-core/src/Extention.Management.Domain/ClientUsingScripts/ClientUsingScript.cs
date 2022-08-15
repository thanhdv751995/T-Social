using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.AccountUsingScripts
{
    public class ClientUsingScript : AuditedAggregateRoot<Guid>
    {
        public Guid  ScriptId { get; set; }
        public Guid ClientId { get; set; }
        public bool IsActive { get; set; }
        public string ErrorDetail { get; set; }
        private ClientUsingScript()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal ClientUsingScript(
               Guid id,
               [NotNull] Guid scripId,
               [NotNull] Guid clientId,
               bool isActive,
               string errorDetail
           )
           : base(id)
        {
            ScriptId = scripId;
            ClientId = clientId;
            IsActive = isActive;
            ErrorDetail = errorDetail;
        }
    }
}

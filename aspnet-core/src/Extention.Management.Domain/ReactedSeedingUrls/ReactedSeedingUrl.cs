using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.ReactedSeedingUrls
{
    public class ReactedSeedingUrl : AuditedAggregateRoot<Guid>
    {
        public Guid ClientId { get; set; }
        public string URL { get; set; }

        private ReactedSeedingUrl()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal ReactedSeedingUrl(
               Guid id,
               [NotNull] Guid clientId,
               [NotNull] string url
           )
           : base(id)
        {
            ClientId = clientId;
            URL = url;
        }
    }
}
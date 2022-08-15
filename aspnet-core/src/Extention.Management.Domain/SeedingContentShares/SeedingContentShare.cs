using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.SeedingContentShares
{
    public class SeedingContentShare : AuditedAggregateRoot<Guid>
    {
        public Guid SeedingId { get; set; }
        public string Content { get; set; }
        private SeedingContentShare()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal SeedingContentShare(
               Guid id,
               [NotNull] Guid seedingId,
               [NotNull] string content

           )
           : base(id)
        {
            SeedingId = seedingId;
            Content = content;
        }
    }
}

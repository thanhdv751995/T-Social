using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.SeedingContents
{
    public class SeedingContent : AuditedAggregateRoot<Guid>
    {
        public Guid SeedingId { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        private SeedingContent()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal SeedingContent(
               Guid id,
               [NotNull] Guid seedingId,
               [NotNull] string content,
               string imageUrl

           )
           : base(id)
        {
            SeedingId = seedingId;
            Content = content;
            ImageUrl = imageUrl;
        }
    }
}

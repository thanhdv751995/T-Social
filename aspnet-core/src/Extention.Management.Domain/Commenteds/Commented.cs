using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.Commenteds
{
    public class Commented : AuditedAggregateRoot<Guid>
    {
        public Guid ClientId { get; set; }
        public string CommentContent { get; set; }
        public string URL { get; set; }
        private Commented()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal Commented(
               Guid id,
               [NotNull] Guid clientId,
               [NotNull] string commentContent,
               [NotNull] string url
           )
           : base(id)
        {
            ClientId = clientId;
            CommentContent = commentContent;
            URL = url;
        }
    }
}

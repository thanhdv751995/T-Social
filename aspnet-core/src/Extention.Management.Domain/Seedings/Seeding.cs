using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.Seedings
{
    public class Seeding : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public int PostsWall { get; set; }
        public int PostsGroup { get; set; }
        public int Comments { get; set; }
        public int Reacts { get; set; }
        public int SharesWall { get; set; }
        public int SharesGroup { get; set; }
        public Guid GroupTypeId { get; set; }
        public string URL { get; set; }
        private Seeding()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal Seeding(
               Guid id,
               [NotNull] string name,
               [NotNull] int postsWall,
               [NotNull] int postsGroup,
               [NotNull] int comments,
               [NotNull] int reacts,
               [NotNull] int sharesWall,
               [NotNull] int sharesGroup,
               [NotNull] Guid groupTypeId,
               string url
           )
           : base(id)
        {
            Name = name;
            PostsWall = postsWall;
            PostsGroup = postsGroup;
            Comments = comments;
            Reacts = reacts;
            SharesWall = sharesWall;
            SharesGroup = sharesGroup;
            GroupTypeId = groupTypeId;
            URL = url;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.Seedings
{
    public class SeedingManager : DomainService
    {
        public Seeding CreateAsync(
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
        {
            return new Seeding(
                GuidGenerator.Create(),
                name,
                postsWall,
                postsGroup,
                comments,
                reacts,
                sharesWall,
                sharesGroup,
                groupTypeId,
                url
            );
        }
    }
}

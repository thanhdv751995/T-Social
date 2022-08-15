using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.SeedingContentComments
{
    public class SeedingContentCommentManager : DomainService
    {
        public SeedingContentComment CreateAsync(
              [NotNull] Guid seedingId,
              [NotNull] string content
              )
        {
            return new SeedingContentComment(
                GuidGenerator.Create(),
                seedingId,
                content
            );
        }
    }
}

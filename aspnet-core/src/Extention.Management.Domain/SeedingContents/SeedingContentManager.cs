using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.SeedingContents
{
    public class SeedingContentManager : DomainService
    {
        public SeedingContent CreateAsync(
              [NotNull] Guid seedingId,
              [NotNull] string content ,
              [NotNull] string imageUrl
              )
        {
            return new SeedingContent(
                GuidGenerator.Create(),
                seedingId,
                content,
                imageUrl
            );
        }
    }
}

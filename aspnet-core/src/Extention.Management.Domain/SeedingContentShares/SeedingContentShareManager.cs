using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.SeedingContentShares
{
    public class SeedingContentShareManager : DomainService
    {
        public SeedingContentShare CreateAsync(
              [NotNull] Guid seedingId,
              [NotNull] string content
              )
        {
            return new SeedingContentShare(
                GuidGenerator.Create(),
                seedingId,
                content
            );
        }
    }
}

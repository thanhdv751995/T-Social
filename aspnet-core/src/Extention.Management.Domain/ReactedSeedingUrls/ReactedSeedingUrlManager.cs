using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.ReactedSeedingUrls
{
    public class ReactedSeedingUrlManager : DomainService
    {
        public ReactedSeedingUrlManager()
        {

        }

        public ReactedSeedingUrl CreateAsync(
            [NotNull] Guid clientId,
            [NotNull] string url
            )
        {
            return new ReactedSeedingUrl(
                GuidGenerator.Create(),
                clientId,
                url
            );
        }
    }
}

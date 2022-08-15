using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.CommentedSeedingUrls
{
    public class CommentedSeedingUrlManager : DomainService
    {
        public CommentedSeedingUrlManager()
        {

        }

        public CommentedSeedingUrl CreateAsync(
            [NotNull] Guid clientId,
            [NotNull] string url
            )
        {
            return new CommentedSeedingUrl(
                GuidGenerator.Create(),
                clientId,
                url
            );
        }
    }
}

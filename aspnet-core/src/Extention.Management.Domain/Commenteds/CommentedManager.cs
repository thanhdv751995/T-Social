using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.Commenteds
{
    public class CommentedManager : DomainService
    {
        public CommentedManager()
        {
        }
        public Commented CreateAsync(
           [NotNull] Guid clientId,
           [NotNull] string commentContent,
           [NotNull] string url
           )
        {
            return new Commented(
                GuidGenerator.Create(),
                clientId,
                commentContent,
                url
            );
        }
    }
}

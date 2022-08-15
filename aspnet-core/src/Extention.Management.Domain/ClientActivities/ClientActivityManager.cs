using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.ClientActivities
{
    public class ClientActivityManager : DomainService
    {
        public ClientActivityManager()
        {
        }
        public ClientActivity CreateAsync(
            [NotNull] string userName,
           [NotNull] string content,
            string url,
            string scriptName
           )
        {
            return new ClientActivity(
                GuidGenerator.Create(),
                userName,
                content,
                url,
                scriptName
            );
        }
    }
}

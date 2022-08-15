using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.Logs
{
    public class ProxyClientsLogManager : DomainService
    {
        public ProxyClientsLogManager()
        {

        }

        public ProxyClientLog CreateAsync(
            [NotNull] string proxyIp,
            [NotNull] string body,
            [NotNull] string method
            )
        {
            return new ProxyClientLog(
                GuidGenerator.Create(),
                proxyIp,
                body,
                method
            );
        }
    }
}

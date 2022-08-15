using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.Proxys
{
    public class ProxyManager : DomainService
    {
        public ProxyManager()
        {
        }
        public Proxy CreateAsync(
            [NotNull] string proxyIp,
            [NotNull] bool isActive
            )
        {
            return new Proxy(
                GuidGenerator.Create(),
                proxyIp,
                isActive
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.PingClientExtensions
{
    public class PingClientExtensionManager : DomainService
    {
        public PingClientExtensionManager()
        {

        }

        public PingClientExtension CreateAsync(
             Guid clientId,
             bool isPingSuccess
            )
        {
            return new PingClientExtension(
                GuidGenerator.Create(),
                clientId,
                isPingSuccess
            );
        }
    }
}

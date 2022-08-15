using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.Proxys
{
    public class Proxy : AuditedAggregateRoot<Guid>
    {
        public string ProxyIp { get; set; }
        public bool IsActive { get; set; }
        private Proxy()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal Proxy(
               Guid id,
               [NotNull] string proxy,
               [NotNull] bool isActive
           )
           : base(id)
        {
            ProxyIp = proxy;
            IsActive = isActive;
        }
    }
}

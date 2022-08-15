using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.Logs
{
    public class ProxyClientLog : AuditedAggregateRoot<Guid>
    {
        public string ProxyIp { get; set; }
        public string Body { get; set; }
        public string Method { get; set; }
        private ProxyClientLog()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal ProxyClientLog(
               Guid id,
               [NotNull] string proxy,
               [NotNull] string body,
               string method
           )
           : base(id)
        {
            ProxyIp = proxy;
            Body = body;
            Method = method;
        }
    }
}

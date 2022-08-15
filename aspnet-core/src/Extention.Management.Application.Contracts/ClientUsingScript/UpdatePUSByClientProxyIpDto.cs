using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ProxyUsingScript
{
    public class UpdatePUSByClientProxyIpDto
    {
        public Guid ScriptId { get; set; }
        public Guid ClientId { get; set; }
        public string Url { get; set; }
    }
}

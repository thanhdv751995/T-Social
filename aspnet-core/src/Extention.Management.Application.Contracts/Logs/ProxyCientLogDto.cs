using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Logs
{
    public class ProxyCientLogDto
    {
        public string ProxyIP { get; set; }
        public string Body { get; set; }
        public DateTime CreationTime { get; set; }
    }
}

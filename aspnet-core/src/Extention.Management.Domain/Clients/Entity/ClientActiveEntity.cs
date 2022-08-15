using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Clients.Entity
{
    public class ClientActiveEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ProxyIp { get; set; }
        public string F2FA { get; set; }
        public string Cookie { get; set; }
        public string isActive { get; set; }
    }
}

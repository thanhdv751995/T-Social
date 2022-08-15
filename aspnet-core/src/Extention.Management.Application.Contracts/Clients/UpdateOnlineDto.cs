using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Clients
{
    public class UpdateOnlineDto
    {
        public Guid ClientId { get; set; }
        public bool isOnline { get; set; }
    }
}

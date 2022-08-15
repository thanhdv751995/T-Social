using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.AccountActives
{
    public class CreateUpdateAccountActiveDto
    {
        public Guid ClientId { get; set; }
        public Guid ProxyId { get; set; }
    }
}

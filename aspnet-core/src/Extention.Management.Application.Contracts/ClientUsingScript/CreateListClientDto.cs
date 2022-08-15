using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ProxyUsingScript
{
    public class CreateListClientDto
    {
        public Guid scriptId { get; set; }
        public List<Guid> listClientId { get; set; }
    }
}

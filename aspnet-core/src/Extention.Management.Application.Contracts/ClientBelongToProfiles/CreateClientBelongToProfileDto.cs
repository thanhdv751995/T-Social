using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ClientBelongToProfiles
{
    public class CreateClientBelongToProfileDto
    {
        public List<Guid> ProfilesClientId { get; set; }
        public Guid ClientId { get; set; }
    }
}

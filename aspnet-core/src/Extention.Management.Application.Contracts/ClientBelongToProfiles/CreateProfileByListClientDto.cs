using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ClientBelongToProfiles
{
    public class CreateProfileByListClientDto
    {
        public List<Guid> ListClientId { get; set; }
        public List<Guid> ProfileCLientId { get; set; }
        
    }
}

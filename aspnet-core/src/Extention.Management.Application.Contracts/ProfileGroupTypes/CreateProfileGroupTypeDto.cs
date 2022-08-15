using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ProfileGroupTypes
{
    public class CreateProfileGroupTypeDto
    {
        public List<Guid> ListProfileId { get; set; }
        public List<Guid> ListGroupTypeId { get; set; }
    }
}

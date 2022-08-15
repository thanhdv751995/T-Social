using Extention.Management.ExtraProperties;
using Extention.Management.Profiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ClientInfomations
{
    public class ClientInfoWithStatus
    {
        public string ProfileName { get; set; }
        public List<Guid> ListIdProfile { get; set; }
        public bool Status { get; set; } = false;
    }
}

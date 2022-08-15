using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Profiles
{
    public class CountProfileDto
    {
        public int Count { get; set; }
        public Dictionary<string, List<ClientProfileWithScriptDto>> Result { get; set; }
    }
}

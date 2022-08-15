using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ClientActivities
{
    public class CreateClientActivityDto
    {
        public string UserName { get; set; }
        public string Content { get; set; }
        public string URL { get; set; }
        public string ScriptName { get; set; }
    }
}

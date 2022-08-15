using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Clients
{
    public class LastActivityDto
    {
        public string ScriptName { get; set; }
        public string URL { get; set; }
        public DateTime TimeActivity { get; set; }
    }
}

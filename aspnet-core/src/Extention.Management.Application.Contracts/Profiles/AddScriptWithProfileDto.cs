using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Profiles
{
    public class AddScriptWithProfileDto
    {
        public string NameProfile { get; set; }
        public List<Guid> ListIdProfile { get; set; }
    }
}

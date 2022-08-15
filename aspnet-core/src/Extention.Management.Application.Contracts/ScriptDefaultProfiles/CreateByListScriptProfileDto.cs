using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ScriptDefaultProfiles
{
    public class CreateByListScriptProfileDto
    {
        public List<Guid> ScriptIds { get; set; }
        public List<Guid> ProfileIds { get; set; }
    }
}

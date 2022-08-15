using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ScriptDefaultProfiles
{
    public class CreateByListScriptDefaultTypeDto
    {
        public Guid ScriptId { get; set; }
        public List<Guid> ProfileIds { get; set; }
    }
}

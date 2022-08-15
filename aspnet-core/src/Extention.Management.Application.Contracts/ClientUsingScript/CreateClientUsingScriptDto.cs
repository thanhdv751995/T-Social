using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ProxyUsingScript
{
    public class CreateClientUsingScriptDto
    {
        public Guid ScriptId { get; set; }
        public Guid ClientId { get; set; }
        public bool IsBackgroundJob { get; set; } = false;
    }
}

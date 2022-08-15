using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ProxyUsingScript
{
    public class CreateByListDto
    {
        public Guid ScriptId { get; set; }
        public List<CreateClientUsingScriptDto> ClientIds { get; set; }
    }
}

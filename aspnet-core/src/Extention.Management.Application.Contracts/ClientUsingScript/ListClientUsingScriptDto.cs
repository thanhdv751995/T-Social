using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ProxyUsingScript
{
    public class ListClientUsingScriptDto
    {
        public Guid Id { get; set; }
        public List<Guid> ClientId { get; set; }
        public List<string> UserName { get; set; }
        public string ScriptName { get; set; }
        public string Value { get; set; }
        public Type.Type Type { get; set; }
        public string TypeName { get; set; }
        public bool IsActive { get; set; }
    }
}

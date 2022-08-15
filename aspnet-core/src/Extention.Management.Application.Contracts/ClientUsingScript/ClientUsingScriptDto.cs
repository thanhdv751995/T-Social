using Extention.Management.ClientBelongToProfiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ProxyUsingScript
{
    public class ClientUsingScriptDto
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string Username { get; set; }
        public string ErrorDetail { get; set; }
        public Guid ScriptId { get; set; }
        public string ScriptName { get; set; }
        public string Value { get; set; }
        public Type.Type Type { get; set; }
        public string TypeName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public List<string> ProfileClient { get; set; }
        public string NameFacebook { get; set; }
    }
}

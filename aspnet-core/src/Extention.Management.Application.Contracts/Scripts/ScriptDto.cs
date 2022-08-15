using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Scripts
{
    public class ScriptDto
    {
        public Guid Id { get; set; }
        public string ScriptName { get; set; }
        public string Value { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public int CountAccRun { get; set; }
        public bool IsSeeding { get; set; } 
        public bool IsDefault { get; set; }
        public Type.Type Type { get; set; }
        public string TypeName { get; set; }
        public List<string> ProfilesName { get; set; } 
        public DateTime CreationTime { get; set; }
    }
}

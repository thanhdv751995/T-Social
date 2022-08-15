using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Scripts
{
    public class CreateUpdateScriptDto
    {
        public string ScriptName { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public Type.Type Type { get; set; }
        public Guid ProfileId { get; set; }
        public int ExtraSeedingCount { get; set; } = 0;
        public string Username { get; set; }
        public Guid GroupTypeId { get; set; }
    }
}

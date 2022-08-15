using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.BackgroundJob
{
    public class ClientSeedingDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string NameFacebook { get; set; }
        public Guid ScriptId { get; set; }
        public string ScriptName { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public DateTime? EnqueueAt { get; set; } = null;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.HangfireJob
{
    public class HangfireJobDto
    {
        public Guid JobId { get; set; }
        public bool IsFinish { get; set; }
        public Guid ClientId { get; set; }
        public string Username { get; set; }
        public string NameFacebook { get; set; }
        public Guid ScriptId { get; set; }
        public string ScriptName { get; set; }
        public string Value { get; set; }
        public Type.Type Type { get; set; }
        public string TypeName { get; set; }
        public DateTime CreationTime { get; set; }
        public int MinuteLate { get; set; }

    }
}

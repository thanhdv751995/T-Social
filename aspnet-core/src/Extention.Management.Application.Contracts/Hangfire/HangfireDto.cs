using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Hangfire
{
    public class HangfireDto
    {
        public Guid ClientId { get; set; }
        public Guid ScriptId { get; set; }
        public string UserName { get; set; }
        public string FacebookName { get; set; }
        public string ScriptName { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public DateTime EnqueueAt { get; set; }
    }
}

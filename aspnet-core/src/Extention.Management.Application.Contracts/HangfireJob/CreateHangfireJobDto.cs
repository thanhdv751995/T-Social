using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.HangfireJob
{
    public class CreateHangfireJobDto
    {
        public Guid JobId { get; set; }
        public Guid ClientId { get; set; }
        public Guid ScriptId { get; set; }
        public bool IsFinish { get; set; } = false;
    }
}

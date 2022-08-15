using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Histories
{
    public class CreateDto
    {
        public Guid ClientId { get; set; }
        public Guid ScriptId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.VirtualMachines
{
    public class VirtualMachineDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; } 
        public DateTime? LastModificationTime { get; set; }
    }
}

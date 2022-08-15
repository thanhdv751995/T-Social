using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.VirtualMachines
{
    public class VirtualMachine : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        private VirtualMachine()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal VirtualMachine(
               Guid id,
               [NotNull] string name,
               bool isActive
           )
           : base(id)
        {
            Name = name;
            IsActive = isActive;
        }
    }
}

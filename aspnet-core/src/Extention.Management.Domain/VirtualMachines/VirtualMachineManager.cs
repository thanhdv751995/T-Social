using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.VirtualMachines
{
    public class VirtualMachineManager : DomainService
    {
        public VirtualMachine CreateAsync(
           [NotNull] string name,
           bool isActive
           )
        {
            return new VirtualMachine(
                GuidGenerator.Create(),
                name,
                isActive
            );
        }
    }
}

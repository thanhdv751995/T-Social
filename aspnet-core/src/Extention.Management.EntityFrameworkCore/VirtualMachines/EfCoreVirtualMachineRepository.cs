using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.VirtualMachines
{
    public class EfCoreVirtualMachineRepository : EfCoreRepository<ManagementDbContext, VirtualMachine, Guid>, IVirtualMachineRepository
    {
        public EfCoreVirtualMachineRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}

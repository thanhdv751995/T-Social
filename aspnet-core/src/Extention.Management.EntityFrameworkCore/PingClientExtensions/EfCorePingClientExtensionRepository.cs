using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.PingClientExtensions
{
    public class EfCorePingClientExtensionRepository : EfCoreRepository<ManagementDbContext, PingClientExtension, Guid>, IPingClientExtensionRepository
    {
        public EfCorePingClientExtensionRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}


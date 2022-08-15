using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.ClientInfomations
{
    public class EfCoreClientInfomationRepository : EfCoreRepository<ManagementDbContext, ClientInfomation, Guid>, IClientInfomationRepository
    {
        public EfCoreClientInfomationRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}

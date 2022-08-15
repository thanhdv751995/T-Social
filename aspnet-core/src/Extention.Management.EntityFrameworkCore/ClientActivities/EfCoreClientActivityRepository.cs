using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.ClientActivities
{
    public class EfCoreClientActivityRepository : EfCoreRepository<ManagementDbContext, ClientActivity, Guid>, IClientActivityRepository
    {
        public EfCoreClientActivityRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}

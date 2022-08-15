using Extention.Management.Clients;
using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.GroupJoins
{
    public class EfCoreGroupJoinRepository : EfCoreRepository<ManagementDbContext, GroupJoin, Guid>, IGroupJoinRepository
    {
        public EfCoreGroupJoinRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}

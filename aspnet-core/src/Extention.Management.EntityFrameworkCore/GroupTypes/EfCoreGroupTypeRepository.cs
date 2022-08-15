using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.GroupTypes
{
    public class EfCoreGroupTypeRepository : EfCoreRepository<ManagementDbContext, GroupType, Guid>, IGroupTypeRepository
    {
        public EfCoreGroupTypeRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}

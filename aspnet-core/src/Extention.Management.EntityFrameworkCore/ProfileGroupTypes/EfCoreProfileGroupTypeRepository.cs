using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.ProfileGroupTypes
{
    public class EfCoreProfileGroupTypeRepository : EfCoreRepository<ManagementDbContext, ProfileGroupType, Guid>, IProfileGroupTypeRepository
    {
        public EfCoreProfileGroupTypeRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}

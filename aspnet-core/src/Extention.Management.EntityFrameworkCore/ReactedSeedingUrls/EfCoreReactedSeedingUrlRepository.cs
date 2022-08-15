using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.ReactedSeedingUrls
{
    public class EfCoreReactedSeedingUrlRepository : EfCoreRepository<ManagementDbContext, ReactedSeedingUrl, Guid>, IReactedSeedingUrlRepository
    {
        public EfCoreReactedSeedingUrlRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}

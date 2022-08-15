using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.SeedingContents
{
    public class EfCoreSeedingContentRepository : EfCoreRepository<ManagementDbContext, SeedingContent, Guid>, ISeedingContentRepository
    {
        public EfCoreSeedingContentRepository(
            IDbContextProvider<ManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}

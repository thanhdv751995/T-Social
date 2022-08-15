using Extention.Management.CommentedSeedingUrls;
using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.CommentedSeedingByUrls
{
    public class EfCoreCommentedSeedingUrlRepository : EfCoreRepository<ManagementDbContext, CommentedSeedingUrl, Guid>,
            ICommentedSeedingUrlRepository
    {
        public EfCoreCommentedSeedingUrlRepository(
        IDbContextProvider<ManagementDbContext> dbContextProvider)
       : base(dbContextProvider)
        {
        }
    }
}

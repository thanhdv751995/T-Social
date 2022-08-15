using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.SeedingContentComments
{
    public class EfCoreSeedingContentCommentRepository : EfCoreRepository<ManagementDbContext, SeedingContentComment, Guid>, ISeedingContentCommentRepository
    {
        public EfCoreSeedingContentCommentRepository(
            IDbContextProvider<ManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
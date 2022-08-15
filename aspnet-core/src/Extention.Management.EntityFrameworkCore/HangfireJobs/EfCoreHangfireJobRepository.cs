using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.HangfireJobs
{
    public class EfCoreHangfireJobRepository : EfCoreRepository<ManagementDbContext, HangfireJob, Guid>, IHangfireJobRepository
    {
        public EfCoreHangfireJobRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}

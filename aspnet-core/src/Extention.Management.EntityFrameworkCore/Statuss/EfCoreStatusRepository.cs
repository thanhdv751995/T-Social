using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.Statuss
{
    public class EfCoreStatusRepository : EfCoreRepository<ManagementDbContext, Status, Guid>, IStatusRepository
    {
        public EfCoreStatusRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}

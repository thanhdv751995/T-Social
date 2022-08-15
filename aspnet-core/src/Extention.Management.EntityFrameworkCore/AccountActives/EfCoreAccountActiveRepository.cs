using Extention.Management.AcountAtives;
using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.AccountActives
{
    class EfCoreAccountActiveRepository : EfCoreRepository<ManagementDbContext, AccountActive, Guid>, IAccountActiveRepository
    {
        public EfCoreAccountActiveRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}

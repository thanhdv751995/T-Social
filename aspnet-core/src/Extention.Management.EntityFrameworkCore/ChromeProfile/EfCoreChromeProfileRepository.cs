using Extention.Management.CChromeProfile;
using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.ChromeProfile
{
    public class EfCoreChromeProfileRepository : EfCoreRepository<ManagementDbContext, CChromeProfile.ChromeProfile, Guid>, IChromeProfileRepository
    {
        public EfCoreChromeProfileRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}

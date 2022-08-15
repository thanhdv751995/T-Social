using Extention.Management.EntityFrameworkCore;
using Extention.Management.ProfileOfClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.ClientBelongToProfiles
{
    public class EfCoreClientBelongToProfileRepository : EfCoreRepository<ManagementDbContext, ClientBelongToProfile, Guid>, IClientBelongToProfileRepository
    {
        public EfCoreClientBelongToProfileRepository(
            IDbContextProvider<ManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}

using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.ClientFriends
{
    public class EfCoreClientFriendRepository : EfCoreRepository<ManagementDbContext, ClientFriend, Guid>, IClientFriendRepository
    {
        public EfCoreClientFriendRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}

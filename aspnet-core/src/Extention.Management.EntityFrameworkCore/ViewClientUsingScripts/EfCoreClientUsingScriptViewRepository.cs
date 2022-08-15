using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.ViewClientUsingScripts
{
    public class EfCoreClientUsingScriptViewRepository : EfCoreRepository<ManagementDbContext, ClientUsingScriptView>, IClientUsingScriptViewRepository
    {
        public EfCoreClientUsingScriptViewRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}

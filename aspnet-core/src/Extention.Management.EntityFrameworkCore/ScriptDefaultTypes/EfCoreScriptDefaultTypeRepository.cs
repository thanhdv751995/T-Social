using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.ScriptDefaultTypes
{
    public class EfCoreScriptDefaultTypeRepository : EfCoreRepository<ManagementDbContext, ScriptDefaultType, Guid>, IScriptDefaultTypeRepository
    {
        public EfCoreScriptDefaultTypeRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}

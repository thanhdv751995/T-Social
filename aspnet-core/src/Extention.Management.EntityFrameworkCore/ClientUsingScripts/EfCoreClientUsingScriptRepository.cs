using Extention.Management.Clients;
using Extention.Management.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Extention.Management.AccountUsingScripts
{
    public class EfCoreClientUsingScriptRepository : EfCoreRepository<ManagementDbContext, ClientUsingScript, Guid>, IClientUsingScriptRepository
    {
        public EfCoreClientUsingScriptRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : 
            base(dbContextProvider)
        {
        }
        public async Task<List<ClientUsingScript>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

#pragma warning disable CA1041 // Provide ObsoleteAttribute message
        [Obsolete]
#pragma warning restore CA1041 // Provide ObsoleteAttribute message
        public async Task<List<ClientUsingScript>> GetListClientActive()
        {
            var query = from clientUsingScript in DbSet
                        select clientUsingScript;
            return await query.ToListAsync();
        }
    }
}

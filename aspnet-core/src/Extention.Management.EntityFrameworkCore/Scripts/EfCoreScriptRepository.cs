using Extention.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Extention.Management.EValue;

namespace Extention.Management.Scripts
{
    public class EfCoreScriptRepository : EfCoreRepository<ManagementDbContext, Script, Guid>, IScriptRepository
    {
        public EfCoreScriptRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }

        public async Task<List<Script>> GetListScript(Value value, Type.Type? typeScript, string filter, string id, string seedingName, int skip, int take)
        {
            if (seedingName.IsNullOrEmpty()) { seedingName = ""; }
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(value != Value.All, x => (value == Value.Default) ? x.IsDefault : !x.IsDefault)
                .Where(x => (typeScript == null || x.Type == typeScript)
                && (filter == "" || x.ScriptName.Contains(filter))
                && x.ScriptName.Contains(seedingName) && x.Id.ToString().Contains(id))
                .OrderByDescending(x => x.CreationTime)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }
        public async Task<List<Script>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    script => (script.ScriptName.Contains(filter))
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}


using Extention.Management.EntityFrameworkCore;
using Extention.Management.Proxys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace ExtensionsManagement.ProxyIps
{
    public class EfCoreProxyRepository : EfCoreRepository<ManagementDbContext, Proxy, Guid>,
            IProxyRepository
    {
        public EfCoreProxyRepository(
           IDbContextProvider<ManagementDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }

        public async Task<List<Proxy>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    proxy => proxy.ProxyIp.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Extention.Management.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Extention.Management.Clients;
using Extention.Management.Clients.Entity;

namespace ExtensionsManagement.ClientFacebooks
{
    public class EfCoreClientRepository : EfCoreRepository<ManagementDbContext, Client, Guid>,
            IClientRepository
    {
        public EfCoreClientRepository(
         IDbContextProvider<ManagementDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }

        public async Task<List<Client>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    client => (client.UserName.Contains(filter) || client.NameFacebook.Contains(filter))
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

#pragma warning disable CA1041 // Provide ObsoleteAttribute message
        [Obsolete]
#pragma warning restore CA1041 // Provide ObsoleteAttribute message
        public async Task<List<Client>> GetListClientActive(string nameClient , string nameProfile , string proxyIp)
        {
            IQueryable<Client> query = await GetDbSetAsync();
            if (proxyIp.IsNullOrEmpty()) { proxyIp = ""; }
            if(nameProfile.IsNullOrEmpty())
            {
               query = from client in DbSet
                            join proxy in DbContext.Proxys on client.ProxyIp equals proxy.ProxyIp
                            where client.IsActive && client.ProxyIp != string.Empty && client.NameFacebook.Contains(nameClient) && client.ProxyIp.Contains(proxyIp)
                            select client;
            }    
            else
            {
                query = from client in DbSet
                        join proxy in DbContext.Proxys on client.ProxyIp equals proxy.ProxyIp
                        join clientProfile in DbContext.ClientBelongToProfiles on client.Id equals clientProfile.ClientId
                        join profile in DbContext.ProfileClients on clientProfile.ProfileClientId equals profile.Id
                        where client.IsActive && client.ProxyIp != string.Empty && client.NameFacebook.Contains(nameClient) && profile.ProfileName.Contains(nameProfile) && client.ProxyIp.Contains(proxyIp)
                        select client;
            }    
            return await query.ToListAsync();
        }
    }
}

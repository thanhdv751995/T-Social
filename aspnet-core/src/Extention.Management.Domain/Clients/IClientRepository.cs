using Extention.Management.Clients.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Extention.Management.Clients
{
    public interface IClientRepository : IRepository<Client, Guid>
    {
        Task<List<Client>> GetListAsync(
          int skipCount,
          int maxResultCount,
          string sorting,
          string filter = null
      );
      Task<List<Client>> GetListClientActive(string nameClient, string nameProfile = "" , string proxyIp = "");
    }
}

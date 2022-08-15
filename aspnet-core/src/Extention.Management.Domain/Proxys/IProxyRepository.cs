using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Extention.Management.Proxys
{
    public interface IProxyRepository : IRepository<Proxy, Guid>
    {
        Task<List<Proxy>> GetListAsync(
         int skipCount,
         int maxResultCount,
         string sorting,
         string filter = null
     );
    }
}

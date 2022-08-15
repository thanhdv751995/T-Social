using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Extention.Management.AccountUsingScripts
{
    public interface IClientUsingScriptRepository : IRepository<ClientUsingScript, Guid>
    {

        Task<List<ClientUsingScript>> GetListAsync(
          int skipCount,
          int maxResultCount,
          string sorting,
          string filter = null
      );
        Task<List<ClientUsingScript>> GetListClientActive();
    }
}

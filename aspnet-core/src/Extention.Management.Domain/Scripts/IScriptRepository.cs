using Extention.Management.EValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Extention.Management.Scripts
{
    public interface IScriptRepository : IRepository<Script, Guid>
    {
        Task<List<Script>> GetListScript(
         Value value = Value.All,
         Type.Type? typeScript = null,
         string filter = "",
         string id = "",
         string seedingName = "",
         int skip = 0,
         int take = 10);
        Task<List<Script>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
  );
    }
}

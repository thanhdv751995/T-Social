using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Extention.Management.ClientActivities
{
    public interface IClientActivityRepository : IRepository<ClientActivity, Guid>
    {
    }
}

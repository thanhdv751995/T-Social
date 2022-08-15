using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Extention.Management.GroupTypes
{
    public interface IGroupTypeRepository : IRepository<GroupType, Guid>
    {

    }
}

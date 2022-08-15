using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Extention.Management.Hub
{
    public interface IHubAppService: IApplicationService
    {
        Task AddConnectionToGroup(CreateConnectionToGroupDto createConnectionToGroupDto);
        Task RemoveConnectionToGroup(CreateConnectionToGroupDto createConnectionToGroupDto);
    }
}

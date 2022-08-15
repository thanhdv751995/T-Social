using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Hub
{
    public class HubAppService : ManagementAppService
    {
        public IHubContext<HubSignalR> _hub;
        public HubAppService(IHubContext<HubSignalR> hub)
        {
            _hub = hub;
        }
        public async Task SendAsync(string method)
        {
            await _hub.Clients.All.SendAsync(method);
        }
        public Task AddConnectionToGroup(CreateConnectionToGroupDto createConnectionToGroupDto)
        {
            if (!createConnectionToGroupDto.GroupName.IsNullOrWhiteSpace())
            {
                return _hub.Groups.AddToGroupAsync(createConnectionToGroupDto.ConnectionId, createConnectionToGroupDto.GroupName);
            }
            else 
                return Task.CompletedTask;
        }
        public Task RemoveConnectionToGroup(CreateConnectionToGroupDto createConnectionToGroupDto)
        {
            if (!createConnectionToGroupDto.GroupName.IsNullOrWhiteSpace())
            {
                return _hub.Groups.RemoveFromGroupAsync(createConnectionToGroupDto.ConnectionId, createConnectionToGroupDto.GroupName);
            }
            else 
                return Task.CompletedTask;
        }
    }
}

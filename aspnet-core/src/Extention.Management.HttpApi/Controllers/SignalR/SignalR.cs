using Extention.Management.Hub;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/signalr")]
    public class SignalR : ManagementController
    {
        private readonly HubAppService _hubAppService;
        public SignalR(HubAppService hubAppService)
        {
            _hubAppService = hubAppService;
        }
        [HttpPost("add-connection-to-group")]
        public Task AddConnectionToGroup([FromBody]CreateConnectionToGroupDto createConnectionToGroupDto)
        {
            return _hubAppService.AddConnectionToGroup(createConnectionToGroupDto);
        }
        [HttpPost("remove-connection-to-group")]
        public Task RemoveConnectionToGroup([FromBody]CreateConnectionToGroupDto createConnectionToGroupDto)
        {
            return _hubAppService.RemoveConnectionToGroup(createConnectionToGroupDto);
        }
    }
}

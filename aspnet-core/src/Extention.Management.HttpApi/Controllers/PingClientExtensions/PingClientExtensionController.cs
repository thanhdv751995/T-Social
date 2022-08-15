using Extention.Management.PingClientExtension;
using Extention.Management.PingClientExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.PingClientExtensions
{
    [Microsoft.AspNetCore.Mvc.Route("api/ping-client-extension")]
    public class PingClientExtensionController : ManagementController
    {
        private readonly PingClientExtensionAppService _pingClientExtensionAppService;
        public PingClientExtensionController(PingClientExtensionAppService pingClientExtensionAppService)
        {
            _pingClientExtensionAppService = pingClientExtensionAppService;
        }

        [HttpPost("create-update-ping-client-extension")]
        public async Task CreateUpdatePingClientExtension([FromBody]CreateUpdatePingClientExtensionDto createUpdatePingClientExtensionDto)
        {
            await _pingClientExtensionAppService.CreateUpdatePingClientExtension(createUpdatePingClientExtensionDto);
        }
    }
}

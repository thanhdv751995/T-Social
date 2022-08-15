using Extention.Management.EStatusType;
using Extention.Management.StatusStore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.StatusStore
{
    [Microsoft.AspNetCore.Mvc.Route("status-store")]
    public class StatusStoreController : ManagementController
    {
        private readonly StatusStoreAppService _statusStoreAppService;
        public StatusStoreController(StatusStoreAppService statusStoreAppService)
        {
            _statusStoreAppService = statusStoreAppService;
        }

        [HttpPost("Create")]
        public async Task CreateAsync([FromBody]CreateStatusStoreDto createStatusStoreDto)
        {
            await _statusStoreAppService.CreateAsync(createStatusStoreDto);
        }
        [HttpGet("status-random")]
        public async Task<StatusStoreDto> GetStatusRandomAsync(StatusType statusType)
        {
            return await _statusStoreAppService.GetStatusRandomAsync(statusType);
        }
    }
}

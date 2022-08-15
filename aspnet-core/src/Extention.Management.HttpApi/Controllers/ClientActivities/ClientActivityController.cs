using Extention.Management.ClientActivities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Controllers.ClientActivities
{
    [Microsoft.AspNetCore.Mvc.Route("api/activity")]
    public class ClientActivityController : ManagementController
    {
        private readonly ClientActivityAppService _clientActivityAppService;
        public ClientActivityController(ClientActivityAppService clientActivityAppService)
        {

            _clientActivityAppService = clientActivityAppService;
        }
        [HttpPost("Create")]
        public async Task<ClientActivityDto> CreateAsync([FromBody]CreateClientActivityDto input)
        {
            var client = await _clientActivityAppService.CreateAsync(input);

            return client;
        }
        [HttpDelete("Delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _clientActivityAppService.DeleteAsync(id);
        }
        [HttpGet("Get-List-Activity-By-UserId")]
        public async Task<PagedResultDto<ClientActivityDto>> GetListActivityByUserId(string userId, int skip, int take)
        {
            var friend = await _clientActivityAppService.GetListActivityByUserId(userId, skip, take);
            return friend;
        }
        [HttpGet("Get-List-History-Activity")]
        public async Task<PagedResultDto<ClientActivityDto>> GetListHistoryActivity(DateTime? startDate, DateTime? endDate, string seedingName)
        {
            return await _clientActivityAppService.GetListHistoryActivity(startDate, endDate, seedingName);
        }
    }
}

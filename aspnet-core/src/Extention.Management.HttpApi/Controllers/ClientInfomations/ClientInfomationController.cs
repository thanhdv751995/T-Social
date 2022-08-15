using Extention.Management.ClientInfomations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Controllers.ClientInfomations
{
    [Microsoft.AspNetCore.Mvc.Route("api/Infomation-Client")]
    public class ClientInfomationController : ManagementController
    {
        private readonly ClientInfomationAppService _clientInfomationAppService;
        public ClientInfomationController(ClientInfomationAppService clientInfomationAppService)
        {

            _clientInfomationAppService = clientInfomationAppService;
        }
        [HttpPost("Create")]
        public async Task<ClientInfomationDto> CreateAsync(CreateUpdateClientInfomationDto input)
        {
            var client = await _clientInfomationAppService.CreateAsync(input);

            return client;
        }
        [HttpDelete("Delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _clientInfomationAppService.DeleteAsync(id);
        }
        [HttpGet("Get-By-Id")]
        public async Task<ClientInfomationDto> GetAsync(Guid id)
        {
            var infoClient = await _clientInfomationAppService.GetAsync(id);
            return infoClient;
        }
        [HttpGet("Get-List-User-Info")]
        public async Task<PagedResultDto<ClientInfomationDto>> GetListInfoUser(int skip, int take)
        {
            var infoClient = await _clientInfomationAppService.GetListInfoUser(skip,take);
            return infoClient;
        }
        [HttpPost("Create-Update-By-List")]
        public async Task CreateUpdateByList([FromBody]List<CreateUpdateClientInfomationDto> input)
        {
            await _clientInfomationAppService.CreateUpdateByList(input);
        }
    }
}

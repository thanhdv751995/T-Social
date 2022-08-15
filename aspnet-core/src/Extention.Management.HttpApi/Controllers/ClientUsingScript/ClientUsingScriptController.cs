using Extention.Management.ProxyUsingScript;
using Extention.Management.Scripts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Controllers.ProxyUsingScript
{
    [Microsoft.AspNetCore.Mvc.Route("api/client-using-script")]
    public class ClientUsingScriptController : ManagementController
    {
        private readonly ClientUsingScriptAppService _proxyUsingScriptAppService;
        public ClientUsingScriptController(ClientUsingScriptAppService proxyUsingScriptAppService)
        {
            _proxyUsingScriptAppService = proxyUsingScriptAppService;
        }

        [HttpPost("Create")]
        public async Task<ClientUsingScriptDto> CreateAsync([FromBody]CreateClientUsingScriptDto createProxyUsingScriptDto)
        {
            var rs = await _proxyUsingScriptAppService.CreateAsync(createProxyUsingScriptDto);

            return rs;
        }
        [HttpPost("Create-By-List")]
        public async Task CreateByList([FromBody]CreateByListDto createByListDto)
        {
            await _proxyUsingScriptAppService.CreateByList(createByListDto);
        }
        [HttpPost("Create-By-ListClient")]
        public async Task CreateByListProxyAsync([FromBody] CreateListClientDto listProxyIp)
        {
            await _proxyUsingScriptAppService.CreateByListProxyAsync(listProxyIp);
        }
        [HttpGet("Get-List")]
        public async Task<PagedResultDto<ClientUsingScriptDto>> GetListAsync(string clientId, string seedingName,string scriptName, string type, string isActive, int skip, int take)
        {
            var rs = await _proxyUsingScriptAppService.GetListAsync(clientId, seedingName,scriptName, type, isActive, skip, take);

            return rs;
        }

        [HttpPut("Update-Active")]
        public async Task UpdateAsync([FromBody] UpdateClientUsingScriptDto updateProxyUsingScriptDto, bool isBackgroundJob, Guid scriptId, Guid clientId)
        {
            await _proxyUsingScriptAppService.UpdateAsync(updateProxyUsingScriptDto, isBackgroundJob, scriptId, clientId);
        }
        [HttpPut("Update-Error-Detail")]
        public async Task UpdateErrorDetail(string scriptId, string clientId)
        {
            await _proxyUsingScriptAppService.UpdateErrorDetail(scriptId, clientId);
        }
        
        [HttpDelete("Delete")]
        public async Task DeleteAsync(Guid Id)
        {
            await _proxyUsingScriptAppService.DeleteAsync(Id);
        }
        [HttpGet("Get-List-By-ListClient")]
        public async Task<PagedResultDto<ListClientUsingScriptDto>> GetListByListProxyIp(int skip, int take)
        {
            var rs = await _proxyUsingScriptAppService.GetListByListProxyIp(skip, take);

            return rs;
        }
        [HttpGet("First-Script-Client-Using")]
        public async Task<ClientUsingScriptDto> GetFirstClientUsingScriptDefaultAsync(string username)
        {
            var rs = await _proxyUsingScriptAppService.GetFirstClientUsingScriptDefaultAsync(username);
            return rs;
        }
        [HttpPut("Update-Active-By-ScriptId-ClientId")]
        public async Task UpdateActiveByScriptId([FromBody]UpdatePUSByClientProxyIpDto updatePUSByScriptProxyIpDto)
        {
            await _proxyUsingScriptAppService.UpdateActiveByScriptId(updatePUSByScriptProxyIpDto);
        }
        [HttpPut("Revoke-Or-Repeat-PUS")]
        public async Task RepeatOrRevokePUSDefault(bool isActive, [FromBody]Guid[] clientIds)
        {
            await _proxyUsingScriptAppService.RepeatOrRevokePUSDefault(isActive, clientIds);
        }
        [HttpPost("Create-By-List-Script-Default")]
        public async Task CreateByListScriptDefaultAsync(Guid clientId)
        {
            await _proxyUsingScriptAppService.CreateByListScriptDefaultAsync(clientId);
        }
        [HttpPost("Schedule-Script")]
        public async Task ScheduleNewScript(UpdatePUSByClientProxyIpDto updatePUSByScriptClientIpDto)
        {
            await _proxyUsingScriptAppService.ScheduleNewScript(updatePUSByScriptClientIpDto);
        }
        [HttpPost("Change-Client-Job")]
        public async Task ChangeClientJob(UpdatePUSByClientProxyIpDto updatePUSByScriptClientIpDto)
        {
            await _proxyUsingScriptAppService.ChangeClientJob(updatePUSByScriptClientIpDto);
        }
        [HttpGet("InvokeNewDefaultScriptAsync")]
        public async Task InvokeNewDefaultScriptAsync(Guid clientId)
        {
            await _proxyUsingScriptAppService.InvokeNewDefaultScriptAsync(clientId);
        }
    }
}

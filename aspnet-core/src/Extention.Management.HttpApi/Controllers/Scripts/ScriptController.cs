using ExtensionsManagement.ClientFacebooks;
using Extention.Management.Clients;
using Extention.Management.Proxy;
using Extention.Management.Scripts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Controllers.Scripts
{
    [Microsoft.AspNetCore.Mvc.Route("api/scripts")]
    public class ScriptController : ManagementController
    {
        private readonly ScriptAppService _scriptAppService;
        public ScriptController(ScriptAppService scriptAppService)
        {
            _scriptAppService = scriptAppService;
        }
        [HttpPost("Create")]
        public async Task<ScriptDto> CreateAsync([FromBody] CreateUpdateScriptDto input)
        {
            var script = await _scriptAppService.CreateAsync(input);

            return script;
        }
        [HttpGet("Get-By-Id")]
        public async Task<ScriptDto> GetAsync(CreateUpdateScriptDto input)
        {
            var client = await _scriptAppService.CreateAsync(input);

            return client;
        }
        /// <summary>
        /// Type = 0 : Get All.
        /// Type = 1 : Get Type = Default.
        /// Type = 2 : Get Type = Not Default.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-List")]
        public async Task<PagedResultDto<ScriptDto>> GetListAsync(GetScriptListDto input, Type.Type? typeScript, string seedingName)
        {
            var client = await _scriptAppService.GetListAsync(input, typeScript, seedingName);

            return client;
        }
        [HttpGet("Get-id")]
        public async Task<ScriptDto> GetAsync(Guid id)
        {
            var client = await _scriptAppService.GetAsync(id);
            return client;
        }

        [HttpGet("Get-List-Script")]
        public async Task<List<ScriptDto>> GetListScript()
        {
            var client = await _scriptAppService.GetListScript();
            return client;
        }
        [HttpGet("Get-List-Script-Default")]
        public async Task<List<ScriptDto>> GetListScriptDefault()
        {
            var client = await _scriptAppService.GetListScriptDefault();

            return client;
        }
        [HttpDelete("Delete")]
        public async Task DeleteAsync([FromBody] deleteAccoutDto id)
        {
            await _scriptAppService.DeleteAsync(id.id);
        }

        [HttpDelete("Delete-Script")]
        public async Task DeleteScriptAsync(Guid Id)
        {
            await _scriptAppService.DeleteScriptAsync(Id);
        }

        [HttpPut("Update-Active")]
        public async Task UpdateActive([FromBody] UpdateActiveScriptDto updateActiveScriptDto)
        {
            await _scriptAppService.UpdateActive(updateActiveScriptDto);
        }
        [HttpPut]
        public async Task UpdateAsync(Guid id, [FromBody] CreateUpdateScriptDto input)
        {
            await _scriptAppService.UpdateAsync(id, input);
        }
    }
}

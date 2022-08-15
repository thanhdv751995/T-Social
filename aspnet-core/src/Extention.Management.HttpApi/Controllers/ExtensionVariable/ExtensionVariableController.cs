using Extention.Management.ExtensionVariable;
using Extention.Management.ExtensionVariables;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.ExtensionVariable
{
    [Microsoft.AspNetCore.Mvc.Route("api/extension-variable")]
    public class ExtensionVariableController : ManagementController
    {
        private readonly ExtensionVariableAppService _extensionVariableAppService;
        public ExtensionVariableController(ExtensionVariableAppService extensionVariableAppService)
        {
            _extensionVariableAppService = extensionVariableAppService;
        }

        [HttpGet("List")]
        public async Task<List<ExtensionVariableDto>> GetListAsync()
        {
            return await _extensionVariableAppService.GetListAsync();
        }
        [HttpPost("Create")]
        public async Task CreateAsync([FromBody]ExtensionVariableCreateDto extensionVariableCreateDto)
        {
            await _extensionVariableAppService.CreateAsync(extensionVariableCreateDto);
        }
        [HttpPut("Update")]
        public async Task UpdateAsync([FromBody]ExtensionVariableCreateDto extensionVariableCreateDto)
        {
            await _extensionVariableAppService.UpdateAsync(extensionVariableCreateDto);
        }
    }
}

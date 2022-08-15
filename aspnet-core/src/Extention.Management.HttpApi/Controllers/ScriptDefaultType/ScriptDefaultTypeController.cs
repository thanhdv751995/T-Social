using Extention.Management.ScriptDefaultProfile;
using Extention.Management.ScriptDefaultProfiles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.ScriptDefaultType
{
    [Microsoft.AspNetCore.Mvc.Route("api/script-default-type")]
    public class ScriptDefaultTypeController : ManagementController
    {
        private readonly ScriptDefaultProfileAppService _scriptDefaultProfileAppService;
        public ScriptDefaultTypeController(ScriptDefaultProfileAppService scriptDefaultProfileAppService)
        {
            _scriptDefaultProfileAppService = scriptDefaultProfileAppService;
        }

        [HttpPost("Create")]
        public async Task CreateAsync([FromBody] CreateScriptDefaultTypeDto createScriptDefaultTypeDto)
        {
            await _scriptDefaultProfileAppService.CreateAsync(createScriptDefaultTypeDto);
        }

        [HttpPost("Create-By-List")]
        public async Task CreateByListAsync([FromBody]CreateByListScriptDefaultTypeDto createByListScriptDefaultTypeDto)
        {
            await _scriptDefaultProfileAppService.CreateByListAsync(createByListScriptDefaultTypeDto);
        }
        [HttpPost("Create-By-List-script-profile")]
        public async Task CreateByListScriptProfile([FromBody]CreateByListScriptProfileDto createByListScriptDefaultTypeDto)
        {
            await _scriptDefaultProfileAppService.CreateByListScriptProfile(createByListScriptDefaultTypeDto);
        }
        [HttpPost("Create-by-script")]
        public async Task CreateProfileByScript(Guid idScript,[FromBody] CreateDeleteProfileOfScriptDto listNameProfiles)
        {
            await _scriptDefaultProfileAppService.CreateProfileByScript(idScript, listNameProfiles);
        }
        [HttpDelete("Delete-by-script")]
        public async Task DeleteProfileByScript(Guid idScript, [FromBody] CreateDeleteProfileOfScriptDto listNameProfiles)
        {
            await _scriptDefaultProfileAppService.DeleteProfileByScript(idScript, listNameProfiles);
        }
    }
}

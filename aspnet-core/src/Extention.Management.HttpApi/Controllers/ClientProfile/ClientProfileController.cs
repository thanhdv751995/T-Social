using Extention.Management.ClientBelongToProfiles;
using Extention.Management.ClientInfomations;
using Extention.Management.Profiles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.ClientProfile
{
    [Microsoft.AspNetCore.Mvc.Route("api/client-profile")]
    public class ClientProfileController : ManagementController
    {
        private readonly ProfileClientAppService _profileClientAppService;
        public ClientProfileController(ProfileClientAppService profileClientAppService)
        {
            _profileClientAppService = profileClientAppService;
        }
        [HttpPost("Create")]
        public async Task<List<Guid>> CreateProfileByListTime([FromBody]CreateUpdateProfileClientDto input)
        {
            return await _profileClientAppService.CreateProfileByListTime(input);
        }
        [HttpPut]
        public async Task UpdateAsync(string profileName,[FromBody] UpdateMutilScriptProfileDto updateMutilScriptProfileDto)
        {
            await _profileClientAppService.UpdateAsync(profileName, updateMutilScriptProfileDto);
        }
        [HttpGet("id")]
        public async Task<ProfileClientDto> GetAsync(Guid id)
        {
            return await _profileClientAppService.GetAsync(id);
        }
        [HttpGet("Get-List")]
        public List<ClientInfoWithStatus> GetList(Guid clientId, string profileName)
        {
            return _profileClientAppService.GetList(clientId, profileName);
        }
        //[HttpGet("Get-List-Profile-String")]
        //public async Task<List<ListProfileStringJoinDto>> GetListProfile()
        //{
        //    return await _profileClientAppService.GetListProfile();
        //}
        [HttpGet("Get-List-Profile-With-Script")]
        public async Task<CountProfileDto> GetListProfileWithScript(string profileName)
        {
            return await _profileClientAppService.GetListProfileWithScript(profileName);
        }
        [HttpGet("Get-List-Name-Profile")]
        public List<AddScriptWithProfileDto> GetListNameProfile(Guid? clientId)
        {
            return  _profileClientAppService.GetListNameProfile(clientId);
        }
        [HttpGet("Get-List-Profile-Of-Script")]
        public List<AddScriptWithProfileDto> GetListProfileOfScript(Guid scriptId)
        {
            return  _profileClientAppService.GetListProfileOfScript(scriptId);
        }
        [HttpDelete]
        public async Task DeleteAsync(string name)
        {
            await _profileClientAppService.DeleteAsync(name);
        }
    }
}

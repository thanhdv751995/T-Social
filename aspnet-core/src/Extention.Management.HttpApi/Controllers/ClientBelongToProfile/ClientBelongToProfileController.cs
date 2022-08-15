using Extention.Management.ClientBelongToProfile;
using Extention.Management.ClientBelongToProfiles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.ClientBelongToProfile
{
    [Microsoft.AspNetCore.Mvc.Route("api/client-belong-to-profile")]
    public class ClientBelongToProfileController : ManagementController
    {
        private readonly ClientBelongToProfileAppService _clientBelongToProfileAppService;
        public ClientBelongToProfileController(ClientBelongToProfileAppService clientBelongToProfileAppService)
        {
            _clientBelongToProfileAppService = clientBelongToProfileAppService;
        }

        [HttpPost("Create")]
        public async Task CreateAsync([FromBody] CreateClientBelongToProfileDto createClientBelongToProfileDto)
        {
            await _clientBelongToProfileAppService.CreateAsync(createClientBelongToProfileDto);
        }
        [HttpPost("Create-By-List-ClientId")]
        public async Task CreateByListClientId([FromBody] CreateProfileByListClientDto createProfileByListClientDto)
        {
            await _clientBelongToProfileAppService.CreateByListClientId(createProfileByListClientDto);
        }
        [HttpPost("Create-Profile-Checked")]
        public async Task CreateProfileChecked(Guid ClientId, [FromBody]AddProfileCheckedDto addProfile)
        {
            await _clientBelongToProfileAppService.CreateProfileChecked(ClientId, addProfile);
        }

        [HttpDelete("Delete")]
        public async Task DeleteManyAsync([FromBody] Guid[] ids)
        {
            await _clientBelongToProfileAppService.DeleteManyAsync(ids);
        }
        [HttpDelete("Delete-Profile-Checked")]
        public async Task DeleteProfileChecked(Guid ClientId,[FromBody] DeleteProfileCheckedDto deleteProfile)
        {
            await _clientBelongToProfileAppService.DeleteProfileChecked(ClientId, deleteProfile);
        }
        [HttpGet("Profiles-By-ClientId")]
        public async Task<List<ClientBelongToProfileDto>> GetClientBelongToProfile(Guid clientId)
        {
            return await _clientBelongToProfileAppService.GetClientBelongToProfile(clientId);
        }

    }
}

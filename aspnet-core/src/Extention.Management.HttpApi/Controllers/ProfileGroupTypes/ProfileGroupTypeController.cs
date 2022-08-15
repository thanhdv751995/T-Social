using Extention.Management.ProfileGroupTypes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.ProfileGroupTypes
{
    [Microsoft.AspNetCore.Mvc.Route("api/profile-group-type")]
    public class ProfileGroupTypeController : ManagementController
    {
        private readonly ProfileGroupTypeAppService _profileGroupTypeAppService;
        public ProfileGroupTypeController(ProfileGroupTypeAppService profileGroupTypeAppService)
        {
            _profileGroupTypeAppService = profileGroupTypeAppService;
        }

        [HttpPost("Create")]
        public async Task CreateAsync([FromBody] CreateProfileGroupTypeDto createProfileGroupTypeDto)
        {
            await _profileGroupTypeAppService.CreateAsync(createProfileGroupTypeDto);
        }

        [HttpDelete("Delete")]
        public async Task UpdateAsync(Guid Id)
        {
            await _profileGroupTypeAppService.DeleteAsync(Id);
        }
    }
}

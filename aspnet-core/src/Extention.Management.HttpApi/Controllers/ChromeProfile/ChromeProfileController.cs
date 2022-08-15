using Extention.Management.ChromeProfile;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.ChromeProfile
{
    [Microsoft.AspNetCore.Mvc.Route("api/chrome-profile")]
    public class ChromeProfileController : ManagementController
    {
        private readonly CProfileAppService _cProfileAppService;
        public ChromeProfileController(CProfileAppService cProfileAppService)
        {
            _cProfileAppService = cProfileAppService;
        }

        [HttpGet("current-profile")]
        public ChromeProfileDto GetChromeProfileAsync()
        {
            return _cProfileAppService.GetCurrentCProfile();
        }
        [HttpPost("create-update-profile")]
        public async Task CreateUpdateCProfile([FromBody]CreateUpdateDto createUpdateDto)
        {
            await _cProfileAppService.CreateUpdateCProfile(createUpdateDto);
        }
    }
}

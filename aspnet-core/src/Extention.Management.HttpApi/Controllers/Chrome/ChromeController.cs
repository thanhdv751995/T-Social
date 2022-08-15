using Extention.Management.Chrome;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.Chrome
{
    [Microsoft.AspNetCore.Mvc.Route("api/chrome")]
    public class ChromeController : ManagementController
    {
        private readonly ChromeAppService _chromeAppService;
        public ChromeController(ChromeAppService chromeAppService)
        {
            _chromeAppService = chromeAppService;
        }
        [HttpGet("Start")]
        public async Task StartChromeAsync(string profile, string url)
        {
            await _chromeAppService.StartChromeAsync(profile, url);
        }

        [HttpGet("New-Chrome-Profile")]
        public async Task NewChromeProfileAsync(string profile, string url)
        {
            await _chromeAppService.NewChromeProfileAsync(profile, url);
        }
    }
}

using Extention.Management.SeedingContent;
using Extention.Management.SeedingContents;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.SeedingContent
{
    [Microsoft.AspNetCore.Mvc.Route("api/seeding-content")]
    public class SeedingContentController : ManagementController
    {
        private readonly SeedingContentAppService _seedingContentAppService;
        public SeedingContentController(SeedingContentAppService seedingContentAppService)
        {
            _seedingContentAppService = seedingContentAppService;
        }

        [HttpPost("Create")]
        public async Task CreateByListAsync([FromBody] SeedingContentListDto seedingContentListDto)
        {
            await _seedingContentAppService.CreateByListAsync(seedingContentListDto);
        }
    }
}

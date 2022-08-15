using Extention.Management.ReactedSeedingUrls;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.ReactedSeedingUrl
{
    [Microsoft.AspNetCore.Mvc.Route("api/reated-seeding-url")]
    public class ReactedSeedingUrlController : ManagementController
    {
        private readonly ReactedSeedingUrlAppService _reactedSeedingUrlAppService;
        public ReactedSeedingUrlController(ReactedSeedingUrlAppService reactedSeedingUrlAppService)
        {
            _reactedSeedingUrlAppService = reactedSeedingUrlAppService;
        }

        [HttpPost("Create")]
        public async Task CreateAsync([FromBody] CreateReactedSeedingUrlDto createReactedSeedingUrlDto)
        {
            await _reactedSeedingUrlAppService.CreateAsync(createReactedSeedingUrlDto);
        }
        [HttpPost("CreateMany")]
        public async Task CreateManyAsync([FromBody] List<CreateReactedSeedingUrlDto> createReactedSeedingUrlDtos)
        {
            await _reactedSeedingUrlAppService.CreateManyAsync(createReactedSeedingUrlDtos);
        }
    }
}

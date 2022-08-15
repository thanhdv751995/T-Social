using Extention.Management.CommentedSeedingUrls;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.CommentedSeedingUrl
{
    [Microsoft.AspNetCore.Mvc.Route("api/commented-seeding-url")]
    public class CommentedSeedingUrlController : ManagementController
    {
        private readonly CommentedSeedingUrlAppService _commentedSeedingUrlAppService; 
        public CommentedSeedingUrlController(CommentedSeedingUrlAppService commentedSeedingUrlAppService)
        {
            _commentedSeedingUrlAppService = commentedSeedingUrlAppService;
        }

        [HttpPost("Create")]
        public async Task CreateAsync([FromBody] CreateCommentedSeedingUrlDto input)
        {
            await _commentedSeedingUrlAppService.CreateAsync(input);
        }
    }
}

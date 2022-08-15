using Extention.Management.Commenteds;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.Commenteds
{
    [Microsoft.AspNetCore.Mvc.Route("api/commented")]
    public class CommentedController : ManagementController
    {
        private readonly CommentedAppService _commentedAppService;
        public CommentedController(CommentedAppService commentedAppService)
        {
            _commentedAppService = commentedAppService;
        }
        [HttpPost("Create")]
        public async Task<CommentedDto> CreateAsync([FromBody] CreateCommentedDto input)
        {
            var client = await _commentedAppService.CreateAsync(input);

            return client;
        }
        [HttpGet("Get-By-Id")]
        public async Task<CommentedDto> GetAsync(Guid clientId)
        {
            var client = await _commentedAppService.GetAsync(clientId);

            return client;
        }
        [HttpGet("Get-List-By-Client")]
        public async Task<List<CommentedDto>> GetListCommentByClient(Guid clientId)
        {
            var client = await _commentedAppService.GetListCommentByClient(clientId);

            return client;
        }
    }
}

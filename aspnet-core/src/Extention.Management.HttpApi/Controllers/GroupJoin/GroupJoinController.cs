using Extention.Management.GroupJoin;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Controllers.GroupJoin
{
    [Microsoft.AspNetCore.Mvc.Route("api/group-join")]
    public class GroupJoinController : ManagementController
    {
        private readonly GroupJoinAppService _groupJoinAppService;
        public GroupJoinController(GroupJoinAppService groupJoinAppService)
        {
            _groupJoinAppService = groupJoinAppService;
        }

        [HttpGet("Get-List")]
        public async Task<PagedResultDto<GroupJoinDto>> GetListAsync(string userName, int take, int skip)
        {
            return await _groupJoinAppService.GetListAsync(userName, take, skip);
        }
        [HttpPost("Create")]
        public async Task CreateAsync([FromBody]CreateGJDto createGJDto)
        {
            await _groupJoinAppService.CreateAsync(createGJDto);
        }
        [HttpGet("List-Group-Random")]
        public async Task<List<string>> GetListGrounpRandomAsync(string userName, Guid groupTypeId, int take)
        {
            return await _groupJoinAppService.GetListGrounpRandomAsync(userName, groupTypeId, take);
        }
    }
}

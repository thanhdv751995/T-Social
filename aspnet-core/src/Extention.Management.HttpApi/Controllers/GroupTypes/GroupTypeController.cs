using Extention.Management.GroupTypes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.GroupTypes
{
    [Microsoft.AspNetCore.Mvc.Route("api/group-type")]
    public class GroupTypeController : ManagementController
    {
        private readonly GroupTypeAppService _groupTypeAppService;
        public GroupTypeController(GroupTypeAppService groupTypeAppService)
        {
            _groupTypeAppService = groupTypeAppService;
        }

        [HttpGet("List")]
        public async Task<List<GroupTypeDto>> GetListAsync(string nameGroupType)
        {
            return await _groupTypeAppService.GetListAsync(nameGroupType);
        }

        [HttpPost("Create")]
        public async Task CreateAsync([FromBody]CreateUpdateGroupTypeDto createUpdateGroupTypeDto)
        {
            await _groupTypeAppService.CreateAsync(createUpdateGroupTypeDto);
        }

        [HttpPut("Update")]
        public async Task UpdateAsync(Guid Id, [FromBody]CreateUpdateGroupTypeDto createUpdateGroupTypeDto)
        {
            await _groupTypeAppService.UpdateAsync(Id, createUpdateGroupTypeDto);
        }
        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await _groupTypeAppService.DeleteAsync(id);
        }
    }
}

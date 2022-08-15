using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Extention.Management.GroupJoin
{
    public interface IGroupJoinAppService : IApplicationService
    {
        Task<PagedResultDto<GroupJoinDto>> GetListAsync(string userName, int take, int skip);
        Task CreateAsync(CreateGJDto createGJDto);
        Task<List<string>> GetListGrounpRandomAsync(string userName, Guid groupTypeId, int? take);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Extention.Management.CommentedSeedingUrls
{
    public interface ICommentedSeedingUrlAppService : IApplicationService
    {
        Task CreateManyAsync(List<CreateCommentedSeedingUrlDto> createCommentedSeedingUrlDtos);
        Task CreateAsync(CreateCommentedSeedingUrlDto createCommentedSeedingUrlDto);
    }
}

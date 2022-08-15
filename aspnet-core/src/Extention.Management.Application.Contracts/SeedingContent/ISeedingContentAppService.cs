using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Extention.Management.SeedingContent
{
    public interface ISeedingContentAppService : IApplicationService
    {
        Task CreateByListAsync(SeedingContentListDto seedingContentListDto);
    }
}

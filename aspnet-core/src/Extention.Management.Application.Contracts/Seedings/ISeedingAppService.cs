using Extention.Management.BackgroundJob;
using Extention.Management.Scripts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Extention.Management.Seedings
{
    public interface ISeedingAppService : IApplicationService
    {
        Task CreateAsync(CreateSeedingDto createSeedingDto);
        Task<PagedResultDto<SeedingDto>> GetListSeeding(string name, int skip, int take, int conditionValue);
        Task<SeedingBackgroundJobDto> ListClientScheduleSeeding(string seedingName);
    }
}

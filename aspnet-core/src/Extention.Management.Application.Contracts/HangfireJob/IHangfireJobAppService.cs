using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Extention.Management.HangfireJob
{
    public interface IHangfireJobAppService: IApplicationService
    {
        Task<PagedResultDto<HangfireJobDto>> GetHangfireJobs(PagedResultRequestDto pagedResultRequestDto, string seedingName, int isFinish, Guid? clientId, int minuteLate);
        Task<PagedResultDto<HangfireJobDto>> GetHangfireJobsUnfinished(PagedResultRequestDto pagedResultRequestDto, string seedingName, Guid? clientId);
    }
}

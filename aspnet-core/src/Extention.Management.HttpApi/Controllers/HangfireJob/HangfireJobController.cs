using Extention.Management.ClientActivities;
using Extention.Management.HangfireJob;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Controllers.HangfireJob
{
    [Microsoft.AspNetCore.Mvc.Route("api/hangfire-job")]
    public class HangfireJobController : ManagementController
    {
        private readonly HangfireJobAppService _hangfireJobAppService;
        public HangfireJobController(HangfireJobAppService hangfireJobAppService) 
        {
            _hangfireJobAppService = hangfireJobAppService;
        }

        [HttpGet("List-Hangfire-Job")]
        public async Task<PagedResultDto<HangfireJobDto>> GetHangfireJobs(PagedResultRequestDto pagedResultRequestDto, string seedingName, int isFinish, Guid? clientId, int minuteLate)
        {
            var hangfireJobs =await  _hangfireJobAppService.GetHangfireJobs(pagedResultRequestDto, seedingName, isFinish, clientId, minuteLate);

            return hangfireJobs;
        }
        [HttpGet("Get-Chart-By-Job")]
        public async Task<List<int>> GetJobForChart(DateTime? startDate, DateTime? endDate)
        {
            return await _hangfireJobAppService.GetJobForChart(startDate, endDate);
        }
        [HttpGet("Get-Data-LineChart")]
        public List<IngredientDto> GetLineChart()
        {
            return _hangfireJobAppService.GetLineChart();
        }
        [HttpGet("List-Hangfire-Job-UnFinished")]
        public async Task<PagedResultDto<HangfireJobDto>> GetHangfireJobsUnfinished(PagedResultRequestDto pagedResultRequestDto, string seedingName, Guid? clientId)
        {
            return await _hangfireJobAppService.GetHangfireJobsUnfinished(pagedResultRequestDto, seedingName, clientId);
        }
    }
}

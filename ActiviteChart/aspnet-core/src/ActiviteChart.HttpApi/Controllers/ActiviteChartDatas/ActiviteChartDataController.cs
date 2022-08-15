using ActiviteChart.ActiviteChartDatas;
using Extention.Management.KafkaProducer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ActiviteChart.Controllers.ActiviteChartDatas
{
    [Microsoft.AspNetCore.Mvc.Route("api/activiteChart")]
    public class ActiviteChartDataController : ActiviteChartController
    {
        private readonly ActiviteChartDataAppService _activiteChartDataAppSerivce;
        public ActiviteChartDataController(ActiviteChartDataAppService activiteChartDataAppSerivce)
        {
            _activiteChartDataAppSerivce = activiteChartDataAppSerivce;
        }
        [HttpPost("Create")]
        public async Task<DataActiviteChartDto> CreateAsync([FromBody] CreateActiviteChartDataDto input)
        {
            var activityClient = await _activiteChartDataAppSerivce.CreateAsync(input);

            return activityClient;
        }
        [HttpGet("Get-List-Activity-By-ClientId")]
        public async Task<DatasetChartActivitiesDto> GetListActivityByClientId(string userName, DateTime? startDate, DateTime? endDate)
        {
            var listActivityClient = await _activiteChartDataAppSerivce.GetListActivityByClientId(userName, startDate, endDate);

            return listActivityClient;
        }
        [HttpGet("Get-List")]
        public async Task<List<DataActiviteChartDto>> GetListAsync()
        {
            var listActivityClient = await _activiteChartDataAppSerivce.GetListAsync();

            return listActivityClient;
        }
        [HttpGet("Get-Logs-Activity")]
        public async Task<PagedResultDto<LogActivityDto>> GetLogActivity(string userName, int skip, int take, DateTime? startDate, DateTime? endDate)
        {
            var listActivityClient = await _activiteChartDataAppSerivce.GetLogActivity(userName, skip, take, startDate, endDate);

            return listActivityClient;
        }
    }
}

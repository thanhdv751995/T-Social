using Extention.Management.Histories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.History
{
    [Microsoft.AspNetCore.Mvc.Route("api/history")]
    public class HistoryController: ManagementController
    {
        private readonly HistoryAppService _historyAppService;
        public HistoryController(HistoryAppService historyAppService)
        {
            _historyAppService = historyAppService;
        }

        [HttpPost("Create")]
        public async Task CreateAsync([FromBody] CreateDto input)
        {
            await _historyAppService.CreateAsync(input);
        }

        [HttpPut("Update-Time-Start")]
        public async Task UpdateTimeStart([FromBody] UpdateDateTimeDto input)
        {
            await _historyAppService.UpdateTimeStart(input);
        }

        [HttpPut("Update-Time-End")]
        public async Task UpdateTimeEnd([FromBody] UpdateDateTimeDto input)
        {
            await _historyAppService.UpdateTimeEnd(input);
        }

        [HttpGet("Get-List")]
        public List<HistoryDto> GetListAsync(int skip, int take, string clientId)
        {
            var histories = _historyAppService.GetListAsync(skip, take, clientId);

            return histories;
        }
        [HttpDelete("Delete-Many")]
        public async Task DeleteManyAsync(Guid scriptId)
        {
            await _historyAppService.DeleteManyAsync(scriptId);
        }
    }
}

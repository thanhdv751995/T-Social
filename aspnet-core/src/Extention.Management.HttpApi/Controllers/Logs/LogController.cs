using Extention.Management.Logs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.Logs
{
    [Microsoft.AspNetCore.Mvc.Route("api/log")]
    public class LogController : ManagementController
    {
        private readonly LogAppService _logAppService;
        public LogController(LogAppService logAppService)
        {
            _logAppService = logAppService;
        }

        [HttpPost("Create-Log")]
        public async Task CreateLogAsync([FromBody] CreateProxyClientLogDto proxyClientLogDto)
        {
            await _logAppService.CreateLogAsync(proxyClientLogDto);
        }

        [HttpGet("Get-Logs")]
        public async Task<List<ProxyCientLogDto>> GetListLogs(string IP, string date,int skip, int take)
        {
            var rs = await _logAppService.GetListLogs(IP, date, skip, take);
            return rs;
        }
    }
}

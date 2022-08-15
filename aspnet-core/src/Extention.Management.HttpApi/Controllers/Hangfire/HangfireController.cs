using Extention.Management.Hangfire;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.Hangfire
{
    [Microsoft.AspNetCore.Mvc.Route("api/hangfire")]
    public class HangfireController : ManagementController
    {
        private readonly HangfireAppService _hangfireAppService;
        public HangfireController(HangfireAppService hangfireAppService)
        {
            _hangfireAppService = hangfireAppService;
        }

        [HttpGet("ClientAtScheduleJob")]
        public Tuple<bool, DateTime?> ClientAtScheduleJob(Guid clientId)
        {
            var hangfireJobs = HangfireAppService.ClientAtScheduleJob(clientId);

            return hangfireJobs;
        }

        [HttpGet("IsClientUsingScriptOrInSchedule")]
        public bool IsClientUsingScriptOrInSchedule(Guid scriptId, Guid clientId)
        {
            var hangfireJobs = _hangfireAppService.IsAnyClientUsingScriptOrInSchedule(scriptId, clientId);

            return hangfireJobs;
        }

        [HttpGet("IsClientOnlineUsingScriptOrInSchedule")]
        public bool IsClientOnlineUsingScriptOrInSchedule(Guid clientId)
        {
            var hangfireJobs = HangfireAppService.IsClientOnlineUsingScriptOrInSchedule(clientId);

            return hangfireJobs;
        }

        [HttpGet("HangfireJob")]
        public async Task<List<HangfireDto>> GetListAsync(string seedingName)
        {
            var hangfireJobs = await _hangfireAppService.GetListAsync(seedingName);

            return hangfireJobs;
        }
    }
}

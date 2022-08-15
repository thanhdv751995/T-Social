using ExtensionsManagement.ClientFacebookDtos;
using Extention.Management.BackgroundJob;
using Extention.Management.Scripts;
using Hangfire.Storage.Monitoring;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.BackgroundJob
{
    [Microsoft.AspNetCore.Mvc.Route("api/background-job")]
    public class BackgroundJobController : ManagementController
    {
        private readonly BackgroundJobAppService _backgroundJobAppService;
        public BackgroundJobController(BackgroundJobAppService backgroundJobAppService)
        {
            _backgroundJobAppService = backgroundJobAppService;
        }
        //[HttpGet("check-any-client-job-schedule")]
        //public Tuple<bool, DateTime?> CheckClientAnyScheduleJob(Guid clientId)
        //{
        //    var rs =_backgroundJobAppService.ClientAtScheduleJob(clientId);
        //    return rs;
        //}
        [HttpGet("InvokeCloseChromeBackgroundJob")]
        public async Task InvokeCloseChromeBackgroundJob()
        {
            await _backgroundJobAppService.InvokeCloseChromeBackgroundJob();
        }
        [HttpGet("CheckClientNotUsingAnySciptInTime")]
        public bool CheckClientNotUsingAnySciptInTime(Guid clientId, int minute)
        {
            return _backgroundJobAppService.CheckClientNotUsingAnySciptInTime(clientId, minute);
        }
        [HttpGet("GetClientsDtoActiveAsync")]
        public async Task<List<ClientDto>> GetClientsDtoActiveAsync()
        {
            return await _backgroundJobAppService.GetClientsDtoActiveAsync();
        }
        [HttpGet("CreateUpdateClientDailyJob")]
        public async Task CreateUpdateClientDailyJob()
        {
            await _backgroundJobAppService.CreateUpdateClientDailyJob();
        }
        [HttpGet("TriggerJob")]
        public void TriggerJob(string jobId)
        {
            _backgroundJobAppService.TriggerJob(jobId);
        }
        [HttpGet("ListRecurringJob")]
        public List<RecurringJobDto> GetListRecurringJob()
        {
            return BackgroundJobAppService.GetListRecurringJob();
        }
        [HttpGet("GetListScriptDefault")]

        public async Task GetListScriptDefaultAsync()
        {
            await _backgroundJobAppService.GetListScriptDefaultAsync();
        }
        [HttpGet("GetInvokeNewScriptClientUsing")]
        public async Task<List<InvokeNewScriptDto>> GetInvokeNewScriptClientUsing()
        {
            return await _backgroundJobAppService.GetInvokeNewScriptClientUsing();
        }
    }
}

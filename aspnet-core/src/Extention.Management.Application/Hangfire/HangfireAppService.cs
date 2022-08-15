using Extention.Management.AccountUsingScripts;
using Extention.Management.Clients;
using Extention.Management.Enums;
using Extention.Management.ProxyUsingScript;
using Extention.Management.Scripts;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Extention.Management.Hangfire
{
    public class HangfireAppService : ManagementAppService
    {
        private readonly IClientUsingScriptRepository _clientUsingScriptsRepository;
        private readonly IScriptRepository _scriptRepository;
        private readonly IClientRepository _clientRepository;
        public HangfireAppService(IClientUsingScriptRepository clientUsingScriptsRepository,
            IScriptRepository scriptRepository,
            IClientRepository clientRepository)
        {
            _clientUsingScriptsRepository = clientUsingScriptsRepository;
            _scriptRepository = scriptRepository;
            _clientRepository = clientRepository;
        }

        public static Tuple<bool, DateTime?> ClientAtScheduleJob(Guid clientId)
        {
            DateTime? dateTimeSchedule = null;
            bool result = false;
            var jobStorage = JobStorage.Current.GetMonitoringApi();

            if(jobStorage.ScheduledCount() > 0)
            {
                bool isScheduledJob = jobStorage.ScheduledJobs(0, (int)jobStorage.ScheduledCount()).Any(x => x.Value.Job != null && x.Value.Job.Method.Name == "CreateAsync"
                && x.Value.Job.Type.Name == "IClientUsingScriptAppService"
                && x.Value.Job.Args[0].As<CreateClientUsingScriptDto>().ClientId == clientId);

                if (isScheduledJob)
                {
                    result = true;
                    var scheduledClientJobDto = jobStorage.ScheduledJobs(0, (int)jobStorage.ScheduledCount()).Where(x => x.Value.Job != null && x.Value.Job.Method.Name == "CreateAsync"
                                            && x.Value.Job.Type.Name == "IClientUsingScriptAppService"
                                            && x.Value.Job.Args[0].As<CreateClientUsingScriptDto>().ClientId == clientId)
                        .OrderByDescending(x => x.Value.EnqueueAt).FirstOrDefault();

                    dateTimeSchedule = scheduledClientJobDto.Value.EnqueueAt.AddHours(7);
                }
            }

            return new Tuple<bool, DateTime?>(result, dateTimeSchedule);
        }

        public bool IsAnyClientUsingScriptOrInSchedule(Guid scriptId, Guid clientId)
        {
            var jobStorage = JobStorage.Current.GetMonitoringApi();
            bool result = false;

            result = _clientUsingScriptsRepository.Any(x => x.ClientId == clientId && x.ScriptId == scriptId);

            bool isScheduledJob = jobStorage.ScheduledJobs(0, (int)jobStorage.ScheduledCount()).Any(x => x.Value.Job != null && x.Value.Job.Method.Name == "CreateAsync"
            && x.Value.Job.Type.Name == "IClientUsingScriptAppService"
            && x.Value.Job.Args[0].As<CreateClientUsingScriptDto>().ClientId == clientId);

            bool isProcessingJob = jobStorage.ProcessingJobs(0, (int)jobStorage.ProcessingCount()).Any(x => x.Value.Job != null && x.Value.Job.Method.Name == "CreateAsync"
            && x.Value.Job.Type.Name == "IClientUsingScriptAppService"
            && x.Value.Job.Args[0].As<CreateClientUsingScriptDto>().ClientId == clientId);

            bool isEnqueuedJob = jobStorage.EnqueuedJobs("default", 0, (int)jobStorage.EnqueuedCount("default")).Any(x => x.Value.Job != null && x.Value.Job.Method.Name == "CreateAsync"
            && x.Value.Job.Type.Name == "IClientUsingScriptAppService"
            && x.Value.Job.Args[0].As<CreateClientUsingScriptDto>().ClientId == clientId);

            if (isScheduledJob || isProcessingJob || isEnqueuedJob)
            {
                result = true;
            }

            return result;
        }

        public static bool IsClientOnlineUsingScriptOrInSchedule(Guid clientId)
        {
            var jobStorage = JobStorage.Current.GetMonitoringApi();
            bool result = false;

            bool isScheduledJob = jobStorage.ScheduledJobs(0, (int)jobStorage.ScheduledCount()).Any(x => x.Value.Job != null && x.Value.Job.Method.Name == "CreateAsync"
            && x.Value.Job.Type.Name == "IClientUsingScriptAppService"
            && x.Value.Job.Args[0].As<CreateClientUsingScriptDto>().ClientId == clientId);

            bool isProcessingJob = jobStorage.ProcessingJobs(0, (int)jobStorage.ProcessingCount()).Any(x => x.Value.Job != null && x.Value.Job.Method.Name == "CreateAsync"
            && x.Value.Job.Type.Name == "IClientUsingScriptAppService"
            && x.Value.Job.Args[0].As<CreateClientUsingScriptDto>().ClientId == clientId);

            bool isEnqueuedJob = jobStorage.EnqueuedJobs("default", 0, (int)jobStorage.EnqueuedCount("default")).Any(x => x.Value.Job != null && x.Value.Job.Method.Name == "CreateAsync"
            && x.Value.Job.Type.Name == "IClientUsingScriptAppService"
            && x.Value.Job.Args[0].As<CreateClientUsingScriptDto>().ClientId == clientId);

            if (isScheduledJob || isProcessingJob || isEnqueuedJob)
            {
                result = true;
            }

            return result;
        }

        public static Tuple<bool, DateTime?> IsAnyCreateScriptSchedule()
        {
            var jobStorage = JobStorage.Current.GetMonitoringApi();
            DateTime? dateTimeSchedule = null;
            bool result = false;

            bool isScheduledJob = jobStorage.ScheduledJobs(0, (int)jobStorage.ScheduledCount()).Any(x => x.Value.Job != null && x.Value.Job.Method.Name == "CreateAsync"
            && x.Value.Job.Type.Name == "IScriptAppService");

            if (isScheduledJob)
            {
                result = true;
                var scheduledClientJobDto = jobStorage.ScheduledJobs(0, (int)jobStorage.ScheduledCount()).Where(x => x.Value.Job != null && x.Value.Job.Method.Name == "CreateAsync"
                                        && x.Value.Job.Type.Name == "IScriptAppService")
                    .OrderByDescending(x => x.Value.EnqueueAt).FirstOrDefault();

                dateTimeSchedule = scheduledClientJobDto.Value.EnqueueAt.AddHours(7);
            }

            return new Tuple<bool, DateTime?>(result, dateTimeSchedule);
        }

        public async Task<List<HangfireDto>> GetListAsync(string seedingName)
        {
            var jobStorage = JobStorage.Current.GetMonitoringApi();

            var scheduledJobList = jobStorage.ScheduledJobs(0, (int)jobStorage.ScheduledCount()).Where(x => x.Value.Job != null && x.Value.Job.Method.Name == "CreateAsync"
            && x.Value.Job.Type.Name == "IClientUsingScriptAppService" && _scriptRepository.Any(s => s.Id == x.Value.Job.Args[0].As<CreateClientUsingScriptDto>().ScriptId && s.ScriptName.Contains(seedingName))).ToList();

            List<HangfireDto> hangfireList = new();

            foreach (var scheduleJob in scheduledJobList)
            {
                var script = await _scriptRepository.GetAsync(scheduleJob.Value.Job.Args[0].As<CreateClientUsingScriptDto>().ScriptId);
                var client = await _clientRepository.GetAsync(scheduleJob.Value.Job.Args[0].As<CreateClientUsingScriptDto>().ClientId);

                HangfireDto hangfireDto = new()
                {
                    ClientId = client.Id,
                    UserName = client.UserName,
                    FacebookName = client.NameFacebook,
                    Value = script.Value,
                    ScriptId = script.Id,
                    ScriptName = script.ScriptName,
                    Type = EnumAppService.GetNameEnum<Type.Type>(script.Type),
                    EnqueueAt = scheduleJob.Value.EnqueueAt
                };

                hangfireList.Add(hangfireDto);
            }

            return hangfireList;
        }
    }
}

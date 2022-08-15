using ExtensionsManagement.ClientFacebookDtos;
using Extention.Management.AccountUsingScripts;
using Extention.Management.Chrome;
using Extention.Management.ClientBelongToProfiles;
using Extention.Management.Clients;
using Extention.Management.Enums;
using Extention.Management.GroupJoins;
using Extention.Management.GroupTypes;
using Extention.Management.Hangfire;
using Extention.Management.Hub;
using Extention.Management.Logs;
using Extention.Management.PingClientExtensions;
using Extention.Management.Profiles;
using Extention.Management.ProxyUsingScript;
using Extention.Management.Randoms;
using Extention.Management.ScriptDefaultTypes;
using Extention.Management.Scripts;
using Extention.Management.Seedings;
using Hangfire;
using Hangfire.Storage;
using Hangfire.Storage.Monitoring;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.BackgroundJob
{
    public class BackgroundJobAppService : ManagementAppService, IBackgroundJobAppService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IClientBelongToProfileRepository _clientBelongToProfileRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IClientUsingScriptRepository _clientUsingScriptsRepository;
        private readonly IScriptRepository _scriptRepository;
        private readonly IProfileClientRepository _profileClientRepository;
        private readonly RandomAppService _randomAppService;
        private readonly IHubContext<HubSignalR> _hub;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly ClientUsingScriptAppService _clientUsingScriptAppService;
        private readonly IScriptDefaultTypeRepository _scriptDefaultTypeRepository;
        private readonly ClientAppService _clientAppService;
        private readonly ChromeAppService _chromeAppService;
        private readonly IGroupJoinRepository _groupJoinRepository;
        private readonly IGroupTypeRepository _groupTypeRepository;
        private readonly ISeedingRepository _seedingRepository;
        private readonly IPingClientExtensionRepository _pingClientExtensionRepository;
        public BackgroundJobAppService(IBackgroundJobClient backgroundJobClient,
            IClientBelongToProfileRepository clientBelongToProfileRepository,
            IClientRepository clientRepository,
            IClientUsingScriptRepository clientUsingScriptsRepository,
            IScriptRepository scriptRepository,
            IProfileClientRepository profileClientRepository,
            RandomAppService randomAppService,
            IHubContext<HubSignalR> hub,
            IRecurringJobManager recurringJobManager,
            ClientUsingScriptAppService clientUsingScriptAppService,
            IScriptDefaultTypeRepository scriptDefaultTypeRepository,
            ClientAppService clientAppService,
            ChromeAppService chromeAppService,
            IGroupJoinRepository groupJoinRepository,
            IGroupTypeRepository groupTypeRepository,
            ISeedingRepository seedingRepository,
            IPingClientExtensionRepository pingClientExtensionRepository)
        {
            _backgroundJobClient = backgroundJobClient;
            _clientBelongToProfileRepository = clientBelongToProfileRepository;
            _clientRepository = clientRepository;
            _clientUsingScriptsRepository = clientUsingScriptsRepository;
            _scriptRepository = scriptRepository;
            _profileClientRepository = profileClientRepository;
            _randomAppService = randomAppService;
            _hub = hub;
            _recurringJobManager = recurringJobManager;
            _clientUsingScriptAppService = clientUsingScriptAppService;
            _scriptDefaultTypeRepository = scriptDefaultTypeRepository;
            _clientAppService = clientAppService;
            _chromeAppService = chromeAppService;
            _groupJoinRepository = groupJoinRepository;
            _groupTypeRepository = groupTypeRepository;
            _seedingRepository = seedingRepository;
            _pingClientExtensionRepository = pingClientExtensionRepository;
        }

        public void EnqueueJob<T>(Expression<Action<T>> action)
        {
            _backgroundJobClient.Enqueue<T>(action);
        }
        public void AddUpdateRecurringJob<T>(string jobName, Expression<Action<T>> action, string expression)
        {
            _recurringJobManager.AddOrUpdate<T>(jobName, action, expression);
        }

        public void CheckClientDailyJob() => AddUpdateRecurringJob<IBackgroundJobAppService>("TSocial-ClientDailyJob", x => x.CreateUpdateClientDailyJob(), "*/5 * * * *");

        public void InvokeCloseChrome() => AddUpdateRecurringJob<IBackgroundJobAppService>("TSocial-InvokeCloseChrome", x => x.InvokeCloseChromeBackgroundJob(), "*/15 * * * *");

        public void InvokeNewScriptClientUsingBackgroundJob() => AddUpdateRecurringJob<IBackgroundJobAppService>("TSocial-InvokeNewScriptClientUsing", x => x.InvokeNewScriptClientUsing(), "*/3 * * * *");

        public void CheckExceptionCloseChromeBackgroundJob() => AddUpdateRecurringJob<IBackgroundJobAppService>("TSocial-CheckExceptionCloseChrome", x => x.CheckExceptionCloseChrome(), "*/10 * * * *");

        public async Task<List<Clients.Client>> GetClientsActiveAsync()
        {
            var currentTime = DateTime.Now;

            List<Clients.Client> listClientActiveTime =
            await _clientRepository.GetListAsync(client => !client.HasCheckPoint && client.IsActive
            && !client.Online && _clientBelongToProfileRepository.Any(clientBelongToProfile => clientBelongToProfile.ClientId == client.Id
            && _profileClientRepository.Any(profile => profile.Id == clientBelongToProfile.ProfileClientId
            && (profile.StartTime * 60 + profile.DuringMinutes) > (currentTime.Hour * 60 + currentTime.Minute)
            && profile.StartTime <= currentTime.Hour)));

            return listClientActiveTime;
        }

        public async Task<List<ClientDto>> GetClientsDtoActiveAsync()
        {
            var currentTime = DateTime.Now;

            List<Clients.Client> listClientActiveTime =
            await _clientRepository.GetListAsync(client => !client.HasCheckPoint && client.IsActive
            && !client.Online && _clientBelongToProfileRepository.Any(clientBelongToProfile => clientBelongToProfile.ClientId == client.Id
            && _profileClientRepository.Any(profile => profile.Id == clientBelongToProfile.ProfileClientId
            && (profile.StartTime * 60 + profile.DuringMinutes) > (currentTime.Hour * 60 + currentTime.Minute)
            && profile.StartTime <= currentTime.Hour)));

            return ObjectMapper.Map<List<Clients.Client>, List<ClientDto>>(listClientActiveTime);
        }

        public async Task<List<Clients.Client>> GetClientsActivedAsync()
        {
            var currentTime = DateTime.Now;

            List<Clients.Client> listClientActiveTime =
            await _clientRepository.GetListAsync(client => !client.HasCheckPoint
            && _clientBelongToProfileRepository.Any(clientBelongToProfile => clientBelongToProfile.ClientId == client.Id
            && _profileClientRepository.Any(profile => profile.Id == clientBelongToProfile.ProfileClientId
            && (profile.StartTime * 60 + profile.DuringMinutes) > (currentTime.Hour * 60 + currentTime.Minute)
            && profile.StartTime <= currentTime.Hour)));

            return listClientActiveTime;
        }

        public async Task GetListScriptDefaultAsync()
        {
            var currentTime = DateTime.Now;
            List<Script> listScriptRandomClientUsing = new();

            //danh sách client thời gian active để mở chrome và chạy kịch bản
            List<Clients.Client> listClientActiveTime = await GetClientsActiveAsync();

            if (listClientActiveTime.Any())
            {
                foreach (Clients.Client clientActive in listClientActiveTime)
                {
                    //thời gian hoạt động nếu <= random(20, 30) phút hoặc random true, false để mở chrome
                    bool activeClient = _clientRepository.Any(client =>
                    _clientBelongToProfileRepository.Any(clientBelongToProfile => clientBelongToProfile.ClientId == client.Id
                    && client.Id == clientActive.Id
                    && _profileClientRepository.Any(profile => profile.Id == clientBelongToProfile.ProfileClientId
                    && (((profile.StartTime * 60 + profile.DuringMinutes) - (currentTime.Hour * 60 + currentTime.Minute)) <= _randomAppService.RandomInt(20, 30))
                    && (profile.StartTime * 60 + profile.DuringMinutes) >= (currentTime.Hour * 60 + currentTime.Minute)
                    && profile.StartTime <= currentTime.Hour)))
                    || _randomAppService.RandomInt(0, 100) > _randomAppService.RandomInt(45, 55);

                    if (activeClient)
                    {
                        //lấy danh sách kịch bản thuộc profile của client
                        List<Script> queryListScriptDefaultClientProfile = (from script in _scriptRepository.Where(x => x.IsDefault && x.IsActive)
                                                                            join scriptDefault in _scriptDefaultTypeRepository on script.Id equals scriptDefault.ScriptId
                                                                            join profile in _profileClientRepository on scriptDefault.ProfileId equals profile.Id
                                                                            join clientBelongProfile in _clientBelongToProfileRepository on profile.Id equals clientBelongProfile.ProfileClientId
                                                                            join client in _clientRepository.Where(x => x.Id == clientActive.Id) on clientBelongProfile.ClientId equals client.Id
                                                                            select script).ToList();

                        queryListScriptDefaultClientProfile.RemoveAll(x => x.Type == Type.Type.CloseChrome);

                        List<Guid> listDistincScriptDefaultClientProfileId = queryListScriptDefaultClientProfile.Select(x => x.Id).Distinct().ToList();

                        List<Script> listScriptDefaultClientProfile = await _scriptRepository.GetListAsync(x => listDistincScriptDefaultClientProfileId.Any(Id => Id == x.Id));

                        await CreateListRandomClientUsingScript(clientActive, listScriptDefaultClientProfile.Count, listScriptDefaultClientProfile, listScriptRandomClientUsing);

                        await CreateRandomClientUsingScript(clientActive, listScriptDefaultClientProfile.Count, listScriptDefaultClientProfile);
                    }
                }
            }
        }

        public async Task CreateUpdateClientDailyJob()
        {
            var currentTime = DateTime.Now;
            List<Script> listScriptRandomClientUsing = new();

            //danh sách client thời gian active để mở chrome và chạy kịch bản
            List<Clients.Client> listClientActiveTime = await GetClientsActiveAsync();

            if (listClientActiveTime.Any())
            {
                foreach (Clients.Client clientActive in listClientActiveTime)
                {
                    //thời gian hoạt động nếu <= random(20, 30) phút hoặc random true, false để mở chrome
                    bool activeClient = _clientRepository.Any(client =>
                    _clientBelongToProfileRepository.Any(clientBelongToProfile => clientBelongToProfile.ClientId == client.Id
                    && client.Id == clientActive.Id
                    && _profileClientRepository.Any(profile => profile.Id == clientBelongToProfile.ProfileClientId
                    && (((profile.StartTime * 60 + profile.DuringMinutes) - (currentTime.Hour * 60 + currentTime.Minute)) <= _randomAppService.RandomInt(20, 30))
                    && (profile.StartTime * 60 + profile.DuringMinutes) >= (currentTime.Hour * 60 + currentTime.Minute)
                    && profile.StartTime <= currentTime.Hour)))
                    || _randomAppService.RandomInt(0, 100) > _randomAppService.RandomInt(30, 50);

                    if (activeClient)
                    {
                        //lấy danh sách kịch bản thuộc profile của client
                        List<Script> queryListScriptDefaultClientProfile = (from script in _scriptRepository.Where(x => x.IsDefault && x.IsActive)
                                                                            join scriptDefault in _scriptDefaultTypeRepository on script.Id equals scriptDefault.ScriptId
                                                                            join profile in _profileClientRepository on scriptDefault.ProfileId equals profile.Id
                                                                            join clientBelongProfile in _clientBelongToProfileRepository on profile.Id equals clientBelongProfile.ProfileClientId
                                                                            join client in _clientRepository.Where(x => x.Id == clientActive.Id) on clientBelongProfile.ClientId equals client.Id
                                                                            select script).ToList();

                        queryListScriptDefaultClientProfile.RemoveAll(x => x.Type == Type.Type.CloseChrome);

                        List<Guid> listDistincScriptDefaultClientProfileId = queryListScriptDefaultClientProfile.Select(x => x.Id).Distinct().ToList();

                        List<Script> listScriptDefaultClientProfile = await _scriptRepository.GetListAsync(x => listDistincScriptDefaultClientProfileId.Any(Id => Id == x.Id));

                        await CreateListRandomClientUsingScript(clientActive, listScriptDefaultClientProfile.Count, listScriptDefaultClientProfile, listScriptRandomClientUsing);

                        await CreateRandomClientUsingScript(clientActive, listScriptDefaultClientProfile.Count, listScriptDefaultClientProfile);
                    }
                }
            }
        }

        public async Task CreateListRandomClientUsingScript(Clients.Client clientActive, int totalCount, List<Script> listScriptDefaultClientProfile, List<Script> listScriptRandom)
        {
            if (_clientRepository.Any(x => x.Id == clientActive.Id && !x.Online))
            {
                for (var i = 0; i < totalCount; i++)
                {
                    //random kịch bản để client chạy
                    int rand = _randomAppService.RandomInt(0, totalCount);

                    while (listScriptRandom.Any(x => x.Id == listScriptDefaultClientProfile[rand].Id))
                    {
                        rand = _randomAppService.RandomInt(0, totalCount);
                    }

                    listScriptRandom.Add(listScriptDefaultClientProfile[rand]);

                    await CreateUpdateClientUsingScript(listScriptDefaultClientProfile[rand], clientActive);
                }

                await _chromeAppService.StartChromeAsync(clientActive.ChromeProfile, "https://www.google.com");

                listScriptRandom.Clear();
            }
        }

        //  random 1 kịch bản để chạy
        public async Task CreateRandomClientUsingScript(Clients.Client clientActive, int totalCount, List<Script> listScriptRandomClientUsing)
        {
            if (!HangfireAppService.ClientAtScheduleJob(clientActive.Id).Item1)
            {
                if (_clientRepository.Any(x => x.Id == clientActive.Id && x.Online
                && !_clientUsingScriptsRepository.Any(x => x.ClientId == clientActive.Id && x.IsActive)))
                {
                    if (_randomAppService.RandomInt(0, 100) > _randomAppService.RandomInt(30, 50))
                    {
                        int rand = _randomAppService.RandomInt(0, totalCount);

                        await CreateUpdateClientUsingScript(listScriptRandomClientUsing[rand], clientActive);
                    }
                }
            }
        }

        public async Task CreateUpdateClientUsingScript(Script script, Clients.Client client)
        {
            CreateClientUsingScriptDto createClientUsingScriptDto = new()
            {
                ClientId = client.Id,
                ScriptId = script.Id,
                IsBackgroundJob = true
            };

            await _clientUsingScriptAppService.CreateAsync(createClientUsingScriptDto);
        }

        public bool CheckClientNotUsingAnySciptInTime(Guid clientId, int minute)
        {
            var currentTime = DateTime.Now;

            bool rs = false;

            if (_clientUsingScriptsRepository.Any(x => x.ClientId == clientId))
            {
                var firstCUS = _clientUsingScriptsRepository.Where(CUS => CUS.ClientId == clientId)
                    .OrderByDescending(c => c.LastModificationTime != null ? c.LastModificationTime : c.CreationTime)
                    .FirstOrDefault();

                if (firstCUS != null)
                {
                    if (firstCUS.LastModificationTime == null)
                    {
                        rs = firstCUS.CreationTime.AddMinutes(minute) < currentTime;
                    }
                    else
                    {
                        rs = firstCUS.LastModificationTime.Value.AddMinutes(minute) < currentTime;
                    }
                }
            }

            return rs;
        }

        public async Task InvokeCloseChromeBackgroundJob()
        {
            var currentTime = DateTime.Now;

            //danh sách client online và không có kịch bản nào
            //và đã hết thời gian hoạt động theo profile
            //và không hoạt động kịch bản trong 10 phút và không còn kịch bản nào chạy trong background job

            var clientsOnlineNotUsingAnyScriptOverTimeActive = await _clientRepository.GetListAsync(x => x.Online && x.IsActive && !x.HasCheckPoint
            //&& !_clientUsingScriptsRepository.Any(CUS => CUS.ClientId == x.Id &&
            //_scriptRepository.Any(s => s.Id == CUS.ScriptId && s.Type == Type.Type.WatchLivestream && CUS.IsActive))
            && _clientBelongToProfileRepository.Any(CBP => CBP.ClientId == x.Id
            && _profileClientRepository.Any(profile => profile.Id == CBP.ProfileClientId
            && (profile.StartTime * 60 + profile.DuringMinutes) < (currentTime.Hour * 60 + currentTime.Minute)
            && profile.StartTime <= currentTime.Hour)));

            var clientsOnlineNotUsingAnyScript = await _clientRepository.GetListAsync(x => x.Online && x.IsActive && !x.HasCheckPoint
            && !_clientUsingScriptsRepository.Any(CUS => CUS.ClientId == x.Id && CUS.ErrorDetail.IsNullOrWhiteSpace()
            && _scriptRepository.Any(s => s.Id == CUS.ScriptId && s.Type == Type.Type.WatchLivestream && CUS.IsActive)));

            clientsOnlineNotUsingAnyScript.RemoveAll(x => clientsOnlineNotUsingAnyScriptOverTimeActive.Any(c => c.Id == x.Id));

            var listClientOnlineCheckCloseChrome = clientsOnlineNotUsingAnyScriptOverTimeActive.Concat(clientsOnlineNotUsingAnyScript);

            var clientsInActivedTime = await GetClientsActivedAsync();

            foreach (var client in listClientOnlineCheckCloseChrome)
            {
                if ((!_clientUsingScriptsRepository.Any(x => x.ClientId == client.Id && x.IsActive)
                && CheckClientNotUsingAnySciptInTime(client.Id, 10) && !clientsInActivedTime.Any(x => x.Id == client.Id)) || CheckClientNotUsingAnySciptInTime(client.Id, 30))
                {
                    var scriptCloseChrome = _scriptRepository.FirstOrDefault(x => x.Type == Type.Type.CloseChrome && x.IsActive);

                    ClientUsingScriptDto clientUsingScriptDto = new()
                    {
                        Id = scriptCloseChrome.Id,
                        ClientId = client.Id,
                        Username = client.UserName,
                        Type = scriptCloseChrome.Type,
                        TypeName = EnumAppService.GetNameEnum<Type.Type>(scriptCloseChrome.Type),
                        Value = scriptCloseChrome.Value,
                        ScriptId = scriptCloseChrome.Id,
                        ScriptName = scriptCloseChrome.ScriptName
                    };

                    UpdateOnlineDto updateOnlineDto = new()
                    {
                        ClientId = client.Id,
                        isOnline = false
                    };

                    await _hub.Clients.Group(client.Id.ToString()).SendAsync("NewScript", clientUsingScriptDto);
                    await _clientAppService.UpdateOnline(updateOnlineDto);
                }
            }
        }

        public async Task InvokeNewScriptClientUsing()
        {
            var listClientActiveInTime = await _clientRepository.GetListAsync(x => x.IsActive && !x.HasCheckPoint && _clientUsingScriptsRepository.Any(CUS => CUS.ClientId == x.Id && CUS.IsActive));

            foreach (var client in listClientActiveInTime)
            {
                if (!HangfireAppService.IsClientOnlineUsingScriptOrInSchedule(client.Id))
                {
                    var firstCUS = await _clientUsingScriptAppService.GetFirstClientUsingScriptDefaultAsync(client.UserName);

                    if (firstCUS != null && !_scriptDefaultTypeRepository.Any(sd => sd.ScriptId == firstCUS.ScriptId)
                        && _seedingRepository.Any(s => firstCUS.ScriptName.Contains(s.Name)) && firstCUS.Type != Type.Type.WatchLivestream)
                    {
                        if (!client.Online)
                        {
                            await _chromeAppService.StartChromeAsync(client.ChromeProfile, "https://www.google.com");
                        }

                        await _hub.Clients.Group(client.Id.ToString()).SendAsync("NewScript", firstCUS);
                    }
                }
            }
        }

        public async Task CheckExceptionCloseChrome()
        {
            var currentTime = DateTime.Now;

            var listClientOnline = await _clientRepository.GetListAsync(x => x.Online && !x.HasCheckPoint && x.IsActive);

            var listClientOnlineCloseChromeException = await _clientRepository.GetListAsync(x => x.Online
            && _pingClientExtensionRepository.Any(p => p.ClientId == x.Id && p.LastModificationTime.HasValue
            ? p.LastModificationTime.Value.AddMinutes(15) < currentTime
            : p.CreationTime.AddMinutes(15) < currentTime));

            foreach (var client in listClientOnline)
            {
                await _hub.Clients.Group(client.Id.ToString()).SendAsync("CheckExceptionCloseChrome", client.Id.ToString());
            }

            foreach(var client in listClientOnlineCloseChromeException)
            {
                await _clientAppService.UpdateOnlineExceptionCloseChrome(new UpdateOnlineDto() { ClientId = client.Id, isOnline = false });
            }
        }

        public async Task<List<InvokeNewScriptDto>> GetInvokeNewScriptClientUsing()
        {
            List<InvokeNewScriptDto> list = new();

            var listClientActiveInTime = await _clientRepository.GetListAsync(x => x.IsActive && !x.HasCheckPoint && _clientUsingScriptsRepository.Any(CUS => CUS.ClientId == x.Id && CUS.IsActive));

            foreach (var client in listClientActiveInTime)
            {
                if (!HangfireAppService.IsClientOnlineUsingScriptOrInSchedule(client.Id))
                {
                    var firstCUS = await _clientUsingScriptAppService.GetFirstClientUsingScriptDefaultAsync(client.UserName);

                    if (firstCUS != null && !_scriptDefaultTypeRepository.Any(sd => sd.ScriptId == firstCUS.ScriptId))
                    {
                        list.Add(new InvokeNewScriptDto() { Name = client.NameFacebook, FirstScript = firstCUS.ScriptName });
                    }
                }
            }

            return list;
        }

        public void TriggerJob(string jobId)
        {
            _recurringJobManager.Trigger(jobId);
        }

        public static List<RecurringJobDto> GetListRecurringJob()
        {
            List<RecurringJobDto> ListRecurringJobDto = new();

            var con = JobStorage.Current.GetConnection();

            var listRecurringJob = con.GetRecurringJobs().Where(x => x.Id.Contains("TSocial")).ToList();

            listRecurringJob.ForEach(x => ListRecurringJobDto.Add(new RecurringJobDto()
            {
                Error = x.Error,
                JobId = x.Id,
                State = x.Error != null ? "Error" : "Active",
                LastJobState = x.LastJobState,
                LastExecution = x.LastExecution.HasValue ? x.LastExecution.Value.AddHours(7) : x.LastExecution,
                NextExecution = x.NextExecution.HasValue ? x.NextExecution.Value.AddHours(7) : x.NextExecution,
                Cron = x.Cron
            }));

            return ListRecurringJobDto;
        }
    }
}

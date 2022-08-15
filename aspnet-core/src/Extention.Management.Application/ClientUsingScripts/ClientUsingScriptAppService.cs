using Extention.Management.AccountUsingScripts;
using Extention.Management.BackgroundJob;
using Extention.Management.Chrome;
using Extention.Management.ClientActivities;
using Extention.Management.ClientBelongToProfiles;
using Extention.Management.Clients;
using Extention.Management.Enums;
using Extention.Management.Hangfire;
using Extention.Management.Histories;
using Extention.Management.Hub;
using Extention.Management.KafkaProducer;
using Extention.Management.Profiles;
using Extention.Management.Proxys;
using Extention.Management.Randoms;
using Extention.Management.ScriptDefaultTypes;
using Extention.Management.Scripts;
using Extention.Management.ViewClientUsingScripts;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Uow;

namespace Extention.Management.ProxyUsingScript
{
    public class ClientUsingScriptAppService : ManagementAppService, IClientUsingScriptAppService
    {
        private readonly IClientUsingScriptRepository _clientUsingScriptsRepository;
        private readonly ClientUsingScriptManager _clientUsingScriptManager;
        private readonly IScriptRepository _scriptRepository;
        private readonly IClientRepository _clientRepository;
        private readonly HistoryAppService _historyAppService;
        public readonly IHubContext<HubSignalR> _hub;
        private readonly IClientBelongToProfileRepository _clientBelongToProfileRepository;
        private readonly IProfileClientRepository _profileClientRepository;
        private readonly IScriptDefaultTypeRepository _scriptDefaultTypeRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly RandomAppService _randomAppService;
        private readonly IConfiguration _configuration;
        private readonly ChromeAppService _chromeAppService;
        private readonly HangfireJobAppService _hangfireJobAppService;
        private readonly IClientActivityRepository _clientActivitiesRepository;
        public ClientUsingScriptAppService(IClientUsingScriptRepository clientUsingScriptsRepository,
            ClientUsingScriptManager clientUsingScriptManager,
            IScriptRepository scriptRepository,
            IClientRepository clientRepository,
            HistoryAppService historyAppService,
            IHubContext<HubSignalR> hub,
            IClientBelongToProfileRepository clientBelongToProfileRepository,
            IProfileClientRepository profileClientRepository,
            IScriptDefaultTypeRepository scriptDefaultTypeRepository,
            IBackgroundJobClient backgroundJobClient,
            RandomAppService randomAppService,
            IConfiguration configuration,
            ChromeAppService chromeAppService,
            HangfireJobAppService hangfireJobAppService,
            IClientActivityRepository clientActivitiesRepository
            )
        {
            _clientUsingScriptsRepository = clientUsingScriptsRepository;
            _clientUsingScriptManager = clientUsingScriptManager;
            _scriptRepository = scriptRepository;
            _clientRepository = clientRepository;
            _historyAppService = historyAppService;
            _hub = hub;
            _clientBelongToProfileRepository = clientBelongToProfileRepository;
            _profileClientRepository = profileClientRepository;
            _scriptDefaultTypeRepository = scriptDefaultTypeRepository;
            _backgroundJobClient = backgroundJobClient;
            _randomAppService = randomAppService;
            _configuration = configuration;
            _chromeAppService = chromeAppService;
            _hangfireJobAppService = hangfireJobAppService;
            _clientActivitiesRepository = clientActivitiesRepository;
        }
        public async Task CreateByListProxyAsync(CreateListClientDto listClientId)
        {
            foreach (var clientId in listClientId.listClientId)
            {
                CreateClientUsingScriptDto createClientUsingScriptDto = new()
                {
                    ClientId = clientId,
                    ScriptId = listClientId.scriptId
                };

                await CreateAsync(createClientUsingScriptDto);
            }
        }
        public async Task CreateByListScriptDefaultAsync(Guid clientId)
        {
            try
            {
                var scripts = await _scriptRepository.GetListAsync(x => !x.IsDefault);

                foreach (var script in scripts)
                {
                    var CUS = _clientUsingScriptManager.CreateAsync(script.Id, clientId, true);
                    await _clientUsingScriptsRepository.InsertAsync(CUS);

                    CreateDto createDto = new()
                    {
                        ClientId = clientId,
                        ScriptId = script.Id
                    };

                    await _historyAppService.CreateAsync(createDto);
                }
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }
        [UnitOfWork]
        public async Task<ClientUsingScriptDto> CreateAsync(CreateClientUsingScriptDto createClientUsingScriptDto)
        {
            if (!_clientRepository.Any(x => x.Id == createClientUsingScriptDto.ClientId) || createClientUsingScriptDto.ClientId == Guid.Empty)
            {
                return null;
            }

            ClientUsingScript clientUsingScript;
            bool isCreate = false;

            if (_clientUsingScriptsRepository.Any(x => x.ScriptId == createClientUsingScriptDto.ScriptId && x.ClientId == createClientUsingScriptDto.ClientId))
            {
                clientUsingScript = _clientUsingScriptsRepository.FirstOrDefault(x => x.ScriptId == createClientUsingScriptDto.ScriptId
                && x.ClientId == createClientUsingScriptDto.ClientId);

                UpdateClientUsingScriptDto updateClientUsingScriptDto = new()
                {
                    Id = clientUsingScript.Id
                };

                await UpdateAsync(updateClientUsingScriptDto, createClientUsingScriptDto.IsBackgroundJob, clientUsingScript.ScriptId, clientUsingScript.ClientId);
            }
            else
            {
                isCreate = true;

                clientUsingScript = _clientUsingScriptManager.CreateAsync(createClientUsingScriptDto.ScriptId, createClientUsingScriptDto.ClientId, true);

                await _clientUsingScriptsRepository.InsertAsync(clientUsingScript);
            }

            var script = await _scriptRepository.GetAsync(x => x.Id == createClientUsingScriptDto.ScriptId);

            ClientUsingScriptDto clientUsingScriptDto = ObjectMapper.Map<ClientUsingScript, ClientUsingScriptDto>(clientUsingScript);

            clientUsingScriptDto.ScriptName = script.ScriptName;
            clientUsingScriptDto.TypeName = EnumAppService.GetNameEnum<Type.Type>(script.Type);
            clientUsingScriptDto.Type = script.Type;
            clientUsingScriptDto.Value = script.Value;

            clientUsingScriptDto.Username = _clientRepository.FirstOrDefault(x => x.Id == createClientUsingScriptDto.ClientId).UserName;

            await _hub.Clients.Group(clientUsingScript.ClientId.ToString()).SendAsync("NewScript", clientUsingScriptDto);

            if (isCreate)
            {
                await InvokeOpenChrome(clientUsingScript.ClientId, createClientUsingScriptDto.IsBackgroundJob, clientUsingScriptDto.Type);
            }

            return clientUsingScriptDto;
        }

        public async Task CreateByList(CreateByListDto createByListDto)
        {
            foreach (var CUS in createByListDto.ClientIds)
            {
                CUS.ScriptId = createByListDto.ScriptId;
                CUS.IsBackgroundJob = false;

                await CreateAsync(CUS);
            }
        }

        public async Task InvokeOpenChrome(Guid clientId, bool isBackgroundJob, Type.Type type)
        {
            if (!isBackgroundJob && type != Type.Type.CloseChrome)
            {
                var client = await _clientRepository.GetAsync(clientId);

                if (!client.Online && client.IsActive && !client.HasCheckPoint)
                {
                    await _chromeAppService.StartChromeAsync(client.ChromeProfile, "https://www.google.com");
                }
            }
        }
        [UnitOfWork]
        public async Task UpdateAsync(UpdateClientUsingScriptDto updateClientUsingScriptDto, bool isBackgoundJob, Guid scriptId, Guid clientId)
        {
            var clientUsingScript = await _clientUsingScriptsRepository.GetAsync(updateClientUsingScriptDto.Id);
            var script = await _scriptRepository.GetAsync(scriptId);
            var client = await _clientRepository.GetAsync(clientId);

            if (script.Type != Type.Type.CloseChrome)
            {
                if (client.Online || isBackgoundJob)
                {
                    clientUsingScript.IsActive = !clientUsingScript.IsActive;
                }
            }
            else
            {
                clientUsingScript.IsActive = !clientUsingScript.IsActive;
            }

            await _clientUsingScriptsRepository.UpdateAsync(clientUsingScript);

            await InvokeOpenChrome(clientUsingScript.ClientId, isBackgoundJob, script.Type);
        }

        public async Task UpdateErrorDetail(string scriptId, string clientId)
        {
            var clientUsingScript = await _clientUsingScriptsRepository.GetAsync(x => x.ScriptId.ToString() == scriptId && x.ClientId.ToString() == clientId);

            if (clientUsingScript != null) clientUsingScript.ErrorDetail = "Bài viết này không thể bình luận";

            await _clientUsingScriptsRepository.UpdateAsync(clientUsingScript);

            await _hub.Clients.All.SendAsync("UpdateErrorDetail", clientUsingScript.Id, clientUsingScript.ErrorDetail);
        }
        
        public async Task InvokeNewDefaultScriptAsync(Guid clientId)
        {
            var client = await _clientRepository.GetAsync(clientId);

            var nextDefaultScript = await GetFirstClientUsingScriptDefaultAsync(client.UserName);

            if (nextDefaultScript != null)
            {
                var clientUsingScript = await GetClientUsingScriptDto(clientId, Guid.Empty);
                if(_scriptDefaultTypeRepository.Any(x => x.ScriptId == nextDefaultScript.ScriptId) && !HangfireAppService.IsClientOnlineUsingScriptOrInSchedule(client.Id))
                {
                    await InvokeNewScript(clientUsingScript);
                }
            }
        }

        public async Task DeleteAsync(Guid Id)
        {
            var clientUsingScript = await _clientUsingScriptsRepository.GetAsync(Id);

            await _clientUsingScriptsRepository.DeleteAsync(clientUsingScript);
        }

        public async Task<PagedResultDto<ClientUsingScriptDto>> GetListAsync(string nameFacebook, string seedingName, string scriptName, string type, string isActive, int skip, int take)
        {
            var result = new PagedResultDto<ClientUsingScriptDto>(
                        0,
                        new List<ClientUsingScriptDto>()
                    );

            if (nameFacebook.IsNullOrWhiteSpace() || nameFacebook == "null")
            {
                nameFacebook = "";
            }

            if (scriptName.IsNullOrWhiteSpace() || scriptName == "null")
            {
                scriptName = "";
            }
            if (seedingName.IsNullOrWhiteSpace() || seedingName == "null")
            {
                seedingName = "";
            }
            var query = from client in _clientRepository.Where(x => x.NameFacebook.Contains(nameFacebook))
                        join clientUsingScript in _clientUsingScriptsRepository.WhereIf(isActive != "null" && !isActive.IsNullOrWhiteSpace() && isActive != null,
                        x => x.IsActive == bool.Parse(isActive))
                        on client.Id equals clientUsingScript.ClientId
                        join script in _scriptRepository.WhereIf(!type.IsNullOrWhiteSpace()
                        && type != "null", x => x.Type == Enum.Parse<Type.Type>(type)).Where(x => x.ScriptName.Contains(scriptName) && x.ScriptName.Contains(seedingName))
                        on clientUsingScript.ScriptId equals script.Id
                        orderby clientUsingScript.CreationTime descending
                        select new { client, clientUsingScript, script };

            var totalCount = query.Count();
            var queryResult = query.Skip(skip).Take(take).ToList();

            try
            {
                    var clientUsingScriptDtos = queryResult.Select(x =>
                    {
                        List<string> listProfileUsingScriptDefault = new();

                        if (x.script.IsDefault)
                        {
                            listProfileUsingScriptDefault = _profileClientRepository.Where(profile =>
                            _scriptDefaultTypeRepository.Any(scriptDefault => profile.Id == scriptDefault.ProfileId
                            && _scriptRepository.Any(script => script.Id == scriptDefault.ScriptId && script.Id == x.script.Id)))
                            .Select(x => x.ProfileName)
                            .ToList();
                        }

                        var clientUsingScriptDto = ObjectMapper.Map<Script, ClientUsingScriptDto>(x.script);

                        clientUsingScriptDto.Id = x.clientUsingScript.Id;
                        clientUsingScriptDto.IsActive = x.clientUsingScript.IsActive;
                        clientUsingScriptDto.ClientId = x.client.Id;
                        clientUsingScriptDto.Username = x.client.UserName;
                        clientUsingScriptDto.ScriptId = x.script.Id;
                        clientUsingScriptDto.ScriptName = x.script.ScriptName;
                        clientUsingScriptDto.Type = x.script.Type;
                        clientUsingScriptDto.Value = x.script.Value;
                        clientUsingScriptDto.TypeName = EnumAppService.GetNameEnum<Type.Type>(x.script.Type);
                        clientUsingScriptDto.LastModificationTime = x.clientUsingScript.LastModificationTime;
                        clientUsingScriptDto.ProfileClient = listProfileUsingScriptDefault;
                        clientUsingScriptDto.NameFacebook = x.client.NameFacebook;
                        clientUsingScriptDto.ErrorDetail = x.clientUsingScript.ErrorDetail;
                        clientUsingScriptDto.CreationTime = x.clientUsingScript.CreationTime;

                        return clientUsingScriptDto;
                    }).ToList();

                    result = new PagedResultDto<ClientUsingScriptDto>(
                        totalCount,
                        clientUsingScriptDtos
                    );
                return result;
                }

            catch
            {
                return result;
            }
        }

        public async Task<PagedResultDto<ListClientUsingScriptDto>> GetListByListProxyIp(int skip, int take)
        {
            var listScriptClientUsing = await _scriptRepository.GetListAsync(x => _clientUsingScriptsRepository.Any(p => p.ScriptId == x.Id && p.IsActive));
            var listScriptClientUsingMap = ObjectMapper.Map<List<Script>, List<ListClientUsingScriptDto>>(listScriptClientUsing);

            listScriptClientUsingMap.ForEach(script =>
            {
                if (script.Value == "")
                {
                    script.ScriptName = script.ScriptName;
                }
                else
                {
                    script.ScriptName = script.ScriptName;
                }
                script.ClientId = _clientRepository.Where(x => _clientUsingScriptsRepository.Any(p => p.ScriptId == script.Id && p.ClientId == x.Id && p.IsActive))
                .Select(x => x.Id).ToList();

                script.UserName = _clientRepository.Where(x => _clientUsingScriptsRepository.Any(p => p.ScriptId == script.Id && p.ClientId == x.Id && p.IsActive))
                .Select(x => x.UserName).ToList();

                script.TypeName = EnumAppService.GetNameEnum<Type.Type>(script.Type);

            });

            listScriptClientUsingMap = listScriptClientUsingMap.Skip(skip).Take(take).ToList();

            return new PagedResultDto<ListClientUsingScriptDto>(
                   listScriptClientUsingMap.Count,
                   listScriptClientUsingMap
            );
        }

        public async Task<ClientUsingScriptDto> GetFirstClientUsingScriptDefaultAsync(string username)
        {
            try
            {
                var clientId = _clientRepository.FirstOrDefault(x => x.UserName == username).Id;

                if (_clientUsingScriptsRepository.Any(x => x.ClientId == clientId
                && (x.ErrorDetail == null || x.ErrorDetail == "")
                && x.IsActive && _scriptRepository.Any(s => s.Id == x.ScriptId && s.IsActive)))
                {
                    var clientUsingScriptDto = await GetClientUsingScriptDto(clientId, Guid.Empty);

                    return clientUsingScriptDto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task<ClientUsingScriptDto> GetClientUsingScriptDto(Guid clientId, Guid scriptId)
        {
            ClientUsingScript firstPUS;

            if (scriptId == Guid.Empty)
            {
                firstPUS = _clientUsingScriptsRepository
                        .Where(x => x.ClientId == clientId && x.IsActive
                        && x.ErrorDetail.IsNullOrWhiteSpace()
                        && _scriptRepository.Any(s => s.Id == x.ScriptId && s.IsActive))
                        .OrderByDescending(x => x.LastModificationTime == null
                        ? x.CreationTime
                        : x.LastModificationTime)
                        .FirstOrDefault();
            }
            else
            {
                firstPUS = _clientUsingScriptsRepository
                        .Where(x => x.ClientId == clientId && x.IsActive && x.ScriptId != scriptId
                        && x.ErrorDetail.IsNullOrWhiteSpace()
                        && _scriptRepository.Any(s => s.Id == x.ScriptId && s.IsActive))
                        .OrderByDescending(x => x.LastModificationTime == null
                        ? x.CreationTime
                        : x.LastModificationTime)
                        .FirstOrDefault();
            }

            var script = await _scriptRepository.GetAsync(firstPUS.ScriptId);

            var scriptMap = ObjectMapper.Map<Script, ClientUsingScriptDto>(script);

            scriptMap.TypeName = EnumAppService.GetNameEnum<Type.Type>(scriptMap.Type);
            scriptMap.ClientId = firstPUS.ClientId;
            scriptMap.ScriptId = firstPUS.ScriptId;
            scriptMap.Username = _clientRepository.FirstOrDefault(x => x.Id == clientId).UserName;

            scriptMap.ProfileClient = _profileClientRepository.GetListAsync(profile =>
            _clientBelongToProfileRepository.Any(clientBelongProfile => clientBelongProfile.ProfileClientId == profile.Id
            && _clientRepository.Any(client => client.Id == clientBelongProfile.ClientId)))
            .Result.Select(x => x.ProfileName).ToList();

            return scriptMap;
        }

        public async Task UpdateActiveByScriptId(UpdatePUSByClientProxyIpDto updatePUSByScriptClientIpDto)
        {
            var PUS = await _clientUsingScriptsRepository.GetAsync(x => x.ScriptId == updatePUSByScriptClientIpDto.ScriptId && x.ClientId == updatePUSByScriptClientIpDto.ClientId);

            PUS.IsActive = false;

            await _clientUsingScriptsRepository.UpdateAsync(PUS);

            await _hangfireJobAppService.UpdateAsync(updatePUSByScriptClientIpDto.ClientId, updatePUSByScriptClientIpDto.ScriptId);

            _backgroundJobClient.Schedule<IClientUsingScriptAppService>(x => x.InvokeNewDefaultScriptAsync(PUS.ClientId), TimeSpan.FromSeconds(_randomAppService.RandomInt(2, 5)));
        }

        public async Task UpdatingScriptCloseChromeHandler(Guid scriptId, Guid clientId)
        {
            var script = await _scriptRepository.GetAsync(scriptId);
            var client = await _clientRepository.GetAsync(clientId);

            if (script.Type == Type.Type.CloseChrome)
            {
                client.Online = false;

                await _clientRepository.UpdateAsync(client);

                _backgroundJobClient.Enqueue<IClientUsingScriptAppService>(x => x.UpdateActiveManyScript(client.Id, false));
            }
        }

        public async Task InvokeNewScript(ClientUsingScriptDto clientUsingScriptDto)
        {
            await _hub.Clients.Group(clientUsingScriptDto.ClientId.ToString()).SendAsync("NewScript", clientUsingScriptDto);
        }

        public async Task ScheduleNewScript(UpdatePUSByClientProxyIpDto updatePUSByScriptClientIpDto)
        {
            if (_clientUsingScriptsRepository.Any(x => x.ClientId == updatePUSByScriptClientIpDto.ClientId && x.IsActive
                && x.ScriptId != updatePUSByScriptClientIpDto.ScriptId
                && _scriptRepository.Any(s => s.Id == x.ScriptId && s.IsActive)))
            {
                var clientUsingScriptDto = await GetClientUsingScriptDto(updatePUSByScriptClientIpDto.ClientId, updatePUSByScriptClientIpDto.ScriptId);

                var clientScheduleLastJob = HangfireAppService.ClientAtScheduleJob(updatePUSByScriptClientIpDto.ClientId);

                if (!clientScheduleLastJob.Item1)
                {
                    _backgroundJobClient.Schedule<IClientUsingScriptAppService>(x => x.InvokeNewScript(clientUsingScriptDto), TimeSpan.FromSeconds(_randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:ScheduleNewScript:Min"]), int.Parse(_configuration["TimeSchedule:ScheduleNewScript:Max"]))));
                }
                else
                {
                    _backgroundJobClient.Schedule<IClientUsingScriptAppService>(x => x.InvokeNewScript(clientUsingScriptDto),
                        TimeSpan.FromSeconds(clientScheduleLastJob.Item2.Value.Second + _randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:ScheduleNewScript:Min"]), int.Parse(_configuration["TimeSchedule:ScheduleNewScript:Max"]))));
                }
            }
        }

        public async Task UpdateActiveManyScript(Guid clientId, bool isActive)
        {
            if (_clientUsingScriptsRepository.Any(x => x.ClientId == clientId && x.IsActive))
            {
                var listClientUsingScriptActive = await _clientUsingScriptsRepository.GetListAsync(x => x.ClientId == clientId && x.IsActive);

                listClientUsingScriptActive.ForEach(x => x.IsActive = isActive);

                await _clientUsingScriptsRepository.UpdateManyAsync(listClientUsingScriptActive);
            }
        }

        public async Task RepeatOrRevokePUSDefault(bool isActive, Guid[] clientIds)
        {
            try
            {
                var PUSs = !clientIds.Any()
                                    ? await _clientUsingScriptsRepository
                                    .GetListAsync(x => _scriptRepository.Any(s => s.Id == x.ScriptId && s.IsDefault))
                                    : await _clientUsingScriptsRepository
                                    .GetListAsync(x => _scriptRepository.Any(s => s.Id == x.ScriptId && s.IsDefault) && clientIds.Contains(x.ClientId));

                PUSs.ForEach(x => x.IsActive = isActive);

                await _clientUsingScriptsRepository.UpdateManyAsync(PUSs);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task ChangeClientJob(UpdatePUSByClientProxyIpDto updatePUSByScriptClientIpDto)
        {
            var clients = await _clientRepository.GetListAsync(x => !_clientActivitiesRepository.Any(c => c.UserName == x.UserName && c.URL == updatePUSByScriptClientIpDto.Url));

            if (clients.Any())
            {
                var randomIndexCLient = _randomAppService.RandomInt(0, clients.Count);

                var jobInSchedule = HangfireAppService.ClientAtScheduleJob(clients[randomIndexCLient].Id);

                var CUSDtoCreate = new CreateClientUsingScriptDto
                {
                    ClientId = clients[randomIndexCLient].Id,
                    ScriptId = updatePUSByScriptClientIpDto.ScriptId,
                    IsBackgroundJob = false
                };

                if (jobInSchedule.Item1)
                {
                    _backgroundJobClient.Schedule<IClientUsingScriptAppService>(x => x.CreateAsync(CUSDtoCreate), TimeSpan.FromSeconds(jobInSchedule.Item2.Value.Second + _randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:ScheduleNewScript:Min"]), int.Parse(_configuration["TimeSchedule:ScheduleNewScript:Max"]))));
                }
                else
                {
                    await CreateAsync(CUSDtoCreate);
                }
            }
        }
    }
}

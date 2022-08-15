using ExtensionsManagement.ClientFacebookDtos;
using Extention.Management.AccountUsingScripts;
using Extention.Management.BackgroundJob;
using Extention.Management.ClientActivities;
using Extention.Management.Clients;
using Extention.Management.CommentedSeedingUrls;
using Extention.Management.Enums;
using Extention.Management.GroupJoins;
using Extention.Management.Hangfire;
using Extention.Management.HangfireJob;
using Extention.Management.HangfireJobs;
using Extention.Management.Histories;
using Extention.Management.ProxyUsingScript;
using Extention.Management.Randoms;
using Extention.Management.ReactedSeedingUrls;
using Extention.Management.ScriptDefaultTypes;
using Extention.Management.Seedings;
using Hangfire;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace Extention.Management.Scripts
{
    public class ScriptAppService : ManagementAppService, IScriptAppService
    {
        private readonly IScriptRepository _scriptRepository;
        private readonly ScriptManager _scriptManager;
        private readonly IClientUsingScriptRepository _clientUsingScriptsRepository;
        private readonly IHistoryRepository _historiesRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly RandomAppService _randomAppService;
        private readonly IClientRepository _clientRepository;
        private readonly ISeedingRepository _seedingRepository;
        private readonly IScriptDefaultTypeRepository _scriptDefaultTypeRepository;
        private readonly IConfiguration _configuration;
        private readonly IGroupJoinRepository _groupJoinRepository;
        private readonly HangfireJobAppService _hangfireJobAppService;
        private readonly CommentedSeedingUrlAppService _commentedSeedingUrlAppService;
        private readonly ICommentedSeedingUrlRepository _commentedSeedingUrlRepository;
        private readonly IHangfireJobRepository _hangfireJobRepository;
        private readonly ReactedSeedingUrlAppService _reactedSeedingUrlAppService;
        private readonly IReactedSeedingUrlRepository _reactedSeedingUrlsRepository;
        public ScriptAppService(IScriptRepository scriptRepository,
         ScriptManager scriptManager,
         ISeedingRepository seedingRepository,
         IScriptDefaultTypeRepository scriptDefaultTypeRepository,
        IClientUsingScriptRepository clientUsingScriptsRepository,
         IHistoryRepository historiesRepository,
         IBackgroundJobClient backgroundJobClient,
         RandomAppService randomAppService,
         IClientRepository clientRepository,
         IConfiguration configuration,
         IGroupJoinRepository groupJoinRepository,
         HangfireJobAppService hangfireJobAppService,
         CommentedSeedingUrlAppService commentedSeedingUrlAppService,
         ICommentedSeedingUrlRepository commentedSeedingUrlRepository,
         IHangfireJobRepository hangfireJobRepository,
         ReactedSeedingUrlAppService reactedSeedingUrlAppService,
         IReactedSeedingUrlRepository reactedSeedingUrlsRepository)
        {
            _seedingRepository = seedingRepository;
            _scriptDefaultTypeRepository = scriptDefaultTypeRepository;
            _scriptRepository = scriptRepository;
            _scriptManager = scriptManager;
            _clientUsingScriptsRepository = clientUsingScriptsRepository;
            _historiesRepository = historiesRepository;
            _backgroundJobClient = backgroundJobClient;
            _randomAppService = randomAppService;
            _clientRepository = clientRepository;
            _configuration = configuration;
            _groupJoinRepository = groupJoinRepository;
            _hangfireJobAppService = hangfireJobAppService;
            _commentedSeedingUrlAppService = commentedSeedingUrlAppService;
            _commentedSeedingUrlRepository = commentedSeedingUrlRepository;
            _hangfireJobRepository = hangfireJobRepository;
            _reactedSeedingUrlAppService = reactedSeedingUrlAppService;
            _reactedSeedingUrlsRepository = reactedSeedingUrlsRepository;
        }
        public async Task<ScriptDto> GetAsync(Guid id)
        {
            var proxy = await _scriptRepository.GetAsync(id);
            return ObjectMapper.Map<Script, ScriptDto>(proxy);
        }

        [UnitOfWork]
        public async Task<ScriptDto> CreateAsync(CreateUpdateScriptDto input)
        {
            if (_scriptRepository.Any(x => x.Type == Type.Type.CloseChrome && input.Type == Type.Type.CloseChrome))
            {
                throw new UserFriendlyException($"Close Chrome already exist");
            }
            if (!_scriptRepository.Any(x => x.ScriptName == input.ScriptName))
            {
                var script = _scriptManager.CreateAsync(input.ScriptName, input.IsActive, input.IsDefault, input.Value, input.Type);

                await _scriptRepository.InsertAsync(script);

                CreateScriptForSeedingByType(script, input.ExtraSeedingCount, input.Username, input.GroupTypeId, input.Type);
                return ObjectMapper.Map<Script, ScriptDto>(script);
            }
            else
            {
                return null;
            };
        }

        public async Task<ScriptDto> CreateHandlerAsync(CreateUpdateScriptDto input)
        {
            if (!_scriptRepository.Any(x => x.ScriptName == input.ScriptName))
            {
                var script = _scriptManager.CreateAsync(input.ScriptName, input.IsActive, input.IsDefault, input.Value, input.Type);

                await _scriptRepository.InsertAsync(script);

                return ObjectMapper.Map<Script, ScriptDto>(script);
            }
            else
            {
                return null;
            };
        }

        public async void CreateScriptForSeedingByType(Script script, int extraSeedingCount, string username, Guid groupTypeId, Type.Type type)
        {
            if (script.Type == Type.Type.ReactionPost
                || script.Type == Type.Type.WatchLivestream
                || script.Type == Type.Type.SharePost
                || script.Type == Type.Type.CommentPost
                || script.Type == Type.Type.SharePostToGroup)
            {
                var splitStringValue = script.Value.Split(_configuration.GetSection("FacebookParameter")["UrlCharacter"]);
                string url = splitStringValue[0];
                //lấy số lần để xử lý (số client xử lý)
                int times = 0;

                if (script.Type == Type.Type.ReactionPost)
                {
                    times = int.Parse(splitStringValue[^2]);
                }

                if (script.Type == Type.Type.WatchLivestream)
                {
                    times = int.Parse(splitStringValue[1]);
                }

                if (script.Type == Type.Type.SharePost || script.Type == Type.Type.CommentPost || script.Type == Type.Type.SharePostToGroup)
                {
                    times = extraSeedingCount;
                }

                //lấy danh sách client để xử lý kịch bản
                var clientsExecute = await GetListClientToExecute(username, groupTypeId, type, times, url);

                if (clientsExecute.Count < times)
                {
                    throw new UserFriendlyException($"Client remain {clientsExecute.Count} < {times}! Try another times!");
                }

                _backgroundJobClient.Enqueue<IScriptAppService>(x => x.SheduleOrEnqueueCreateScriptJobAsync(script.Id, times, clientsExecute, script.Type));
            }
        }

        public async Task<List<Guid>> GetListClientToExecute(string username, Guid groupTypeId, Type.Type type, int times, string url)
        {
            List<Guid> listClientExecute;

            if (type == Type.Type.SharePostToGroup)
            {
                //client đã giam gia nhóm = groupType để xử lý
                listClientExecute = _clientRepository.Where(x => !x.HasCheckPoint && x.IsActive && x.UserName != username
                && _groupJoinRepository.Any(g => g.Username == x.UserName && g.Username != username && g.GroupTypeId == groupTypeId))
                .OrderBy(x => x.LastModificationTime)
                .Select(x => x.Id)
                .ToList();
            }
            else if (type == Type.Type.CommentPost)
            {
                listClientExecute = _clientRepository.Where(x => !x.HasCheckPoint && x.IsActive && x.UserName != username
                && !_commentedSeedingUrlRepository.Any(c => c.URL == url && c.ClientId == x.Id))
                .OrderBy(x => x.LastModificationTime)
                .Select(x => x.Id)
                .ToList();
            }
            else if (type == Type.Type.ReactionPost)
            {
                listClientExecute = _clientRepository.Where(x => !x.HasCheckPoint && x.IsActive && x.UserName != username
                && !_reactedSeedingUrlsRepository.Any(r => r.URL == url && r.ClientId == x.Id))
                .OrderBy(x => x.LastModificationTime)
                .Select(x => x.Id)
                .ToList();
            }
            else
            {
                listClientExecute = _clientRepository.Where(x => !x.HasCheckPoint && x.IsActive && x.UserName != username)
                .OrderBy(x => x.LastModificationTime)
                .Select(x => x.Id)
                .ToList();
            }

            while (listClientExecute.Count > times)
            {
                var ramdomIndexClient = _randomAppService.RandomInt(0, listClientExecute.Count);

                listClientExecute.RemoveAt(ramdomIndexClient);
            }

            if (type == Type.Type.CommentPost)
            {
                List<CreateCommentedSeedingUrlDto> listCreateCommentedSeedingUrlDtos = new();

                foreach (var clientId in listClientExecute)
                {
                    listCreateCommentedSeedingUrlDtos.Add(new CreateCommentedSeedingUrlDto() { ClientId = clientId, URL = url });
                }

                await _commentedSeedingUrlAppService.CreateManyAsync(listCreateCommentedSeedingUrlDtos);
            }
            else if (type == Type.Type.ReactionPost)
            {
                List<CreateReactedSeedingUrlDto> listCreateReactedSeedingUrlDtos = new();

                foreach (var clientId in listClientExecute)
                {
                    listCreateReactedSeedingUrlDtos.Add(new CreateReactedSeedingUrlDto() { ClientId = clientId, URL = url });
                }

                await _reactedSeedingUrlAppService.CreateManyAsync(listCreateReactedSeedingUrlDtos);
            }

            return listClientExecute;
        }

        public async Task SheduleOrEnqueueCreateScriptJobAsync(Guid scriptId, int times, List<Guid> listClientSelectIdLikePost, Type.Type type)
        {
            int timeRandomExcutedPost = _randomAppService.RandomTime(times);

            await CreateClientUsingScriptAndHangfireJob(listClientSelectIdLikePost, scriptId, type, timeRandomExcutedPost);
        }

        public async Task CreateClientUsingScriptAndHangfireJob(List<Guid> listClientSelectIdLikePost, Guid scriptId, Type.Type type, int timeRandomExcutedPost)
        {
            foreach (var clientId in listClientSelectIdLikePost)
            {
                if (clientId != Guid.Empty)
                {
                    string jobId = string.Empty;
                    CreateClientUsingScriptDto createClientUsingScriptDto = new()
                    {
                        ClientId = clientId,
                        ScriptId = scriptId,
                        IsBackgroundJob = false
                    };

                    var clientScheduleLastJob = HangfireAppService.ClientAtScheduleJob(clientId);

                    if (type == Type.Type.ReactionPost || type == Type.Type.SharePost || type == Type.Type.CommentPost || type == Type.Type.SharePostToGroup)
                    {
                        jobId = _backgroundJobClient.Schedule<IClientUsingScriptAppService>(x =>
                        x.CreateAsync(createClientUsingScriptDto), !clientScheduleLastJob.Item1
                        ? TimeSpan.FromSeconds(_randomAppService.RandomInt(30, timeRandomExcutedPost * 60 + 30))
                        : TimeSpan.FromSeconds(clientScheduleLastJob.Item2.Value.Second + _randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:ScheduleNewScript:Min"]), int.Parse(_configuration["TimeSchedule:ScheduleNewScript:Max"]))));
                    }
                    else if (type == Type.Type.WatchLivestream)
                    {
                        jobId = _backgroundJobClient.Schedule<IClientUsingScriptAppService>(x =>
                        x.CreateAsync(createClientUsingScriptDto), TimeSpan.FromSeconds(_randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:WatchLivestream:Min"]), int.Parse(_configuration["TimeSchedule:WatchLivestream:Max"]))));
                    }
                    var jobType = jobId.GetType();

                    if (jobType == typeof(string))
                    {
                        CreateHangfireJobDto createHangfireJobDto = new()
                        {
                            ClientId = createClientUsingScriptDto.ClientId,
                            ScriptId = createClientUsingScriptDto.ScriptId,
                            JobId = Guid.Parse(jobId)
                        };

                        await _hangfireJobAppService.CreateAsync(createHangfireJobDto);
                    }
                }
            }
        }

        public async Task<PagedResultDto<ScriptDto>> GetListAsync(GetScriptListDto input, Type.Type? typeScript, string seedingName)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Script.CreationTime);
            }

            if (input.Filter.IsNullOrWhiteSpace() || input.Filter == "null")
            {
                input.Filter = "";
            }
            if (input.ID.IsNullOrWhiteSpace() || input.ID == "null")
            {
                input.ID = "";
            }
            if (seedingName.IsNullOrWhiteSpace() || seedingName == "null")
            {
                seedingName = "";
            }
            var totalCount = _scriptRepository.WhereIf(input.Value != EValue.Value.All, x => input.Value == EValue.Value.Default ? x.IsDefault : !x.IsDefault)
                .Where(x => (typeScript == null || x.Type == typeScript)
                && (input.Filter == "" || x.ScriptName.Contains(input.Filter))
                && x.ScriptName.Contains(seedingName) && x.Id.ToString().Contains(input.ID)).Count();

            var script = await _scriptRepository.GetListScript(input.Value, typeScript, input.Filter, input.ID, seedingName, input.SkipCount, input.MaxResultCount);
            //var totalCount = script.Count;
            var scriptsMap = ObjectMapper.Map<List<Script>, List<ScriptDto>>(script);
            scriptsMap.ForEach(x =>
            {
                x.IsSeeding = _seedingRepository.Any(s => x.ScriptName.Contains(s.Name));
                x.TypeName = EnumAppService.GetNameEnum<Type.Type>(x.Type);
                x.Status = _clientUsingScriptsRepository.Any(c => c.IsActive && c.ScriptId == x.Id) ? "Đang chạy" : "Chưa chạy";
                x.CountAccRun = _clientUsingScriptsRepository.Count(c => c.ScriptId == x.Id);
                // x.IsSeeding = false;
            });

            return new PagedResultDto<ScriptDto>(
                totalCount,
                scriptsMap
            );
        }

        public async Task<List<ScriptDto>> GetListScript()
        {
            var listScript = await _scriptRepository.GetListAsync(script => !_seedingRepository.Any(seeding => script.ScriptName.Contains(seeding.Name)));

            return ObjectMapper.Map<List<Script>, List<ScriptDto>>(listScript);
        }
        public async Task DeleteAsync(Guid[] id)
        {
            await _scriptRepository.DeleteManyAsync(id);
        }
        public async Task DeleteScriptAsync(Guid Id)
        {
            try
            {
                if (_scriptRepository.Any(x => x.Id == Id && x.Type == Type.Type.CloseChrome))
                {
                    throw new UserFriendlyException("Script close Chrome cannot delete");
                }
                if (_clientUsingScriptsRepository.Any(x => x.ScriptId == Id))
                {
                    throw new UserFriendlyException("Script is using! Cannot delete");
                }
                if (_historiesRepository.Any(x => x.ScriptId == Id))
                {
                    var histories = await _historiesRepository.GetListAsync(x => x.ScriptId == Id);
                    await _historiesRepository.DeleteManyAsync(histories);
                }
                if (_hangfireJobRepository.Any(x => x.ScriptId == Id))
                {
                    var hangfire = await _hangfireJobRepository.GetListAsync(x => x.ScriptId == Id);
                    await _hangfireJobRepository.DeleteManyAsync(hangfire);
                }
                if (_scriptDefaultTypeRepository.Any(x => x.ScriptId == Id))
                {
                    var scriptDefaultTypes = await _scriptDefaultTypeRepository.GetListAsync(x => x.ScriptId == Id);
                    await _scriptDefaultTypeRepository.DeleteManyAsync(scriptDefaultTypes);
                }
                await _scriptRepository.DeleteAsync(Id);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task UpdateActive(UpdateActiveScriptDto updateActiveScriptDto)
        {
            var script = await _scriptRepository.GetAsync(updateActiveScriptDto.Id);

            script.IsActive = !script.IsActive;
            await _scriptRepository.UpdateAsync(script);
        }

        public async Task UpdateAsync(Guid id, CreateUpdateScriptDto input)
        {
            if (_clientUsingScriptsRepository.Any(x => x.ScriptId == id))
            {
                throw new UserFriendlyException("kịch bản đã chạy không thể sửa");
            }
            var script = await _scriptRepository.GetAsync(id);
            script.ScriptName = input.ScriptName;
            script.Value = input.Value;
            script.Type = input.Type;
            script.IsActive = input.IsActive;
            script.IsDefault = input.IsDefault;
            await _scriptRepository.UpdateAsync(script);
        }

        public async Task<List<ScriptDto>> GetListScriptDefault()
        {
            var result = await _scriptRepository.GetListAsync(x => x.IsDefault);
            return ObjectMapper.Map<List<Script>, List<ScriptDto>>(result);
        }
    }
}

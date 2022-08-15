using Extention.Management.AccountUsingScripts;
using Extention.Management.BackgroundJob;
using Extention.Management.ClientActivities;
using Extention.Management.Clients;
using Extention.Management.Enums;
using Extention.Management.GroupJoins;
using Extention.Management.GroupTypes;
using Extention.Management.Hangfire;
using Extention.Management.HangfireJob;
using Extention.Management.HangfireJobs;
using Extention.Management.ProxyUsingScript;
using Extention.Management.Randoms;
using Extention.Management.Scripts;
using Extention.Management.SeedingContent;
using Extention.Management.SeedingContentComment;
using Extention.Management.SeedingContentComments;
using Extention.Management.SeedingContents;
using Extention.Management.SeedingContentShare;
using Extention.Management.SeedingContentShares;
using Hangfire;
using Hangfire.Storage.Monitoring;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Seedings
{
    public class SeedingAppService : ManagementAppService, ISeedingAppService
    {
        private readonly ISeedingRepository _seedingRepository;
        private readonly SeedingManager _seedingManager;
        private readonly SeedingContentAppService _seedingContentAppService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IClientRepository _clientRepository;
        private readonly IClientActivityRepository _clientActivitiesRepository;
        private readonly ScriptAppService _scriptAppService;
        private readonly IGroupJoinRepository _groupJoinRepository;
        private readonly RandomAppService _randomAppService;
        private readonly ISeedingContentRepository _seedingContentRepository;
        private readonly SeedingContentShareAppService _seedingContentShareAppService;
        private readonly IScriptRepository _scriptRepository;
        private readonly IClientUsingScriptRepository _clientUsingScriptsRepository;
        private readonly IConfiguration _configuration;
        private readonly HangfireJobAppService _hangfireJobAppService;
        private readonly SeedingContentCommentAppService _seedingContentCommentAppService;
        private readonly ISeedingContentCommentRepository _seedingContentCommentRepository;
        private readonly ISeedingContentShareRepository _seedingContentShareRepository;
        private readonly IHangfireJobRepository _hangfireJobRepository;
        private readonly IGroupTypeRepository _groupTypeRepository;

        public SeedingAppService(ISeedingRepository seedingRepository,
            SeedingManager seedingManager,
            SeedingContentAppService seedingContentAppService,
            IBackgroundJobClient backgroundJobClient,
            IClientRepository clientRepository,
            IClientActivityRepository clientActivitiesRepository,
            ScriptAppService scriptAppService,
            IGroupJoinRepository groupJoinRepository,
            RandomAppService randomAppService,
            ISeedingContentRepository seedingContentRepository,
            IScriptRepository scriptRepository,
            IClientUsingScriptRepository clientUsingScriptsRepository,
            IConfiguration configuration,
            HangfireJobAppService hangfireJobAppService,
            SeedingContentShareAppService seedingContentShareAppService,
            SeedingContentCommentAppService seedingContentCommentAppService,
            ISeedingContentCommentRepository seedingContentCommentRepository,
            ISeedingContentShareRepository seedingContentShareRepository,
            IHangfireJobRepository hangfireJobRepository,
            IGroupTypeRepository groupTypeRepository
            )
        {
            _seedingRepository = seedingRepository;
            _seedingManager = seedingManager;
            _seedingContentAppService = seedingContentAppService;
            _backgroundJobClient = backgroundJobClient;
            _clientRepository = clientRepository;
            _clientActivitiesRepository = clientActivitiesRepository;
            _scriptAppService = scriptAppService;
            _groupJoinRepository = groupJoinRepository;
            _randomAppService = randomAppService;
            _seedingContentRepository = seedingContentRepository;
            _scriptRepository = scriptRepository;
            _clientUsingScriptsRepository = clientUsingScriptsRepository;
            _configuration = configuration;
            _hangfireJobAppService = hangfireJobAppService;
            _seedingContentShareAppService = seedingContentShareAppService;
            _seedingContentCommentAppService = seedingContentCommentAppService;
            _seedingContentCommentRepository = seedingContentCommentRepository;
            _seedingContentShareRepository = seedingContentShareRepository;
            _hangfireJobRepository = hangfireJobRepository;
            _groupTypeRepository = groupTypeRepository;
        }

        public async Task CreateAsync(CreateSeedingDto createSeedingDto)
        {
            if (_seedingRepository.Any(x => x.Name == createSeedingDto.Name))
            {
                throw new UserFriendlyException($"Seeding ${createSeedingDto.Name} already exist! Try another name!");
            }

            var seeding = _seedingManager.CreateAsync(
                createSeedingDto.Name,
                createSeedingDto.PostsWall,
                createSeedingDto.PostsGroup,
                createSeedingDto.Comments,
                createSeedingDto.Reacts,
                createSeedingDto.SharesWall,
                createSeedingDto.SharesGroup,
                createSeedingDto.GroupTypeId,
                createSeedingDto.URL);

            await _seedingRepository.InsertAsync(seeding);

            SeedingContentListDto seedingContentListDto = new()
            {
                SeedingId = seeding.Id,
                SeedingContentDtos = createSeedingDto.SeedingContentDtos
            };

            SeedingContentCommentListDto seedingContentCommentListDto = new()
            {
                SeedingId = seeding.Id,
                SeedingContentDtos = createSeedingDto.CommentContentList
            };

            SeedingContentShareListDto seedingContentShareListDto = new()
            {
                SeedingId = seeding.Id,
                SeedingContentDtos = createSeedingDto.ShareContentList
            };

            await _seedingContentCommentAppService.CreateByListAsync(seedingContentCommentListDto);
            await _seedingContentShareAppService.CreateByListAsync(seedingContentShareListDto);
            await _seedingContentAppService.CreateByListAsync(seedingContentListDto);

            await CreateScriptForSeedingAsync(seeding, seedingContentListDto);
        }

        public async Task CreateScriptForSeedingAsync(Seeding seeding, SeedingContentListDto seedingContentListDto)
        {
            //lấy danh sách lient có thể xử lý theo groupType này
            var listClientRelativeSeeding = await _clientRepository.GetListAsync(client => client.IsActive && !client.HasCheckPoint
            && _groupJoinRepository.Any(gj => client.UserName == gj.Username && gj.GroupTypeId == seeding.GroupTypeId));

            var clientsCount = await _clientRepository.CountAsync();

            List<Guid> listClientExecuted = new();

            if ((listClientRelativeSeeding.Count < seeding.PostsGroup) || (listClientRelativeSeeding.Count < seeding.PostsWall))
            {
                throw new UserFriendlyException($"Số tài khoản đã tham gia loại group " +
                    $"là {listClientRelativeSeeding.Count} < số tài khoản để Post vào group {seeding.PostsGroup} hoặc Post vào tường {seeding.PostsWall}");
            }

            if (seeding.URL.IsNullOrWhiteSpace())
            {
                if (seeding.PostsWall > 0)
                {
                    await CreateScriptAsync(seedingContentListDto, seeding.PostsWall, Type.Type.PostStatus, $"Post Wall: {seeding.Name}", listClientRelativeSeeding, listClientExecuted, seeding.GroupTypeId);
                    listClientExecuted.Clear();
                }

                if (seeding.PostsGroup > 0)
                {
                    await CreateScriptAsync(seedingContentListDto, seeding.PostsGroup, Type.Type.PostGroup, $"Post Group: {seeding.Name}", listClientRelativeSeeding, listClientExecuted, seeding.GroupTypeId);
                    listClientExecuted.Clear();
                }

                if (seeding.Reacts > (clientsCount - 1) || seeding.Comments > (clientsCount - 1) || seeding.SharesWall > (clientsCount - 1))
                {
                    throw new UserFriendlyException($"Tổng Số tài khoản " +
                    $"là {(clientsCount - 1)} < số tài khoản để Bày tỏ cảm xúc hoặc Bình luận hoặc Chia sẻ lên tường");
                }
            }
            else
            {
                if (seeding.Reacts > clientsCount || seeding.Comments > clientsCount || seeding.SharesWall > clientsCount)
                {
                    throw new UserFriendlyException($"Tổng Số tài khoản " +
                    $"là {clientsCount} < số tài khoản để Bày tỏ cảm xúc hoặc Bình luận hoặc Chia sẻ lên tường");
                }
            }
        }

        public async Task CreateScriptAsync(
            SeedingContentListDto seedingContentListDto,
            int totalCount,
            Type.Type type,
            string scriptName,
            List<Clients.Client> clients,
            List<Guid> clientsExecuted,
            Guid groupTypeId)
        {
            var countListClientPerContent = (decimal)totalCount / (decimal)seedingContentListDto.SeedingContentDtos.Count;
            List<Guid> listScripIdtDto = new();

            if (type == Type.Type.PostStatus)
            {
                await CreateScriptPostStatus(scriptName, seedingContentListDto, listScripIdtDto);
            }
            else
            {
                await CreateScriptPostGroup(scriptName, seedingContentListDto, listScripIdtDto, groupTypeId);
            }

            List<Guid> listScripIdtDtoExcuted = new();

            await RandomScriptForClientUsingAsync(totalCount, listScripIdtDto, listScripIdtDtoExcuted, countListClientPerContent, clients, clientsExecuted);
        }

        public async Task RandomScriptForClientUsingAsync(
            int totalCount,
            List<Guid> listScripIdtDto,
            List<Guid> listScripIdtDtoExcuted,
            decimal clientsPerContentCount,
            List<Clients.Client> clients,
            List<Guid> clientsExecuted)
        {
            for (var i = 0; i < totalCount; i++)
            {
                var randomIndexScript = _randomAppService.RandomInt(0, listScripIdtDto.Count);

                while (listScripIdtDtoExcuted.Count(x => x == listScripIdtDto[randomIndexScript]) >= clientsPerContentCount)
                {
                    randomIndexScript = _randomAppService.RandomInt(0, listScripIdtDto.Count);
                }

                await RandomClientToUsingScriptAsync(clients, clientsExecuted, listScripIdtDto[randomIndexScript], totalCount);

                listScripIdtDtoExcuted.Add(listScripIdtDto[randomIndexScript]);
            }
        }

        public async Task RandomClientToUsingScriptAsync(List<Clients.Client> clients, List<Guid> clientsExecuted, Guid scriptId, int totalCount)
        {
            var randomClientIndex = _randomAppService.RandomInt(0, clients.Count);

            while (clientsExecuted.Any(x => x == clients[randomClientIndex].Id))
            {
                randomClientIndex = _randomAppService.RandomInt(0, clients.Count);
            }

            clientsExecuted.Add(clients[randomClientIndex].Id);

            if (clients[randomClientIndex].Id != Guid.Empty)
            {
                CreateClientUsingScriptDto createClientUsingScriptDto = new()
                {
                    ClientId = clients[randomClientIndex].Id,
                    ScriptId = scriptId,
                    IsBackgroundJob = false
                };

                await SheduleCreateClientUsingScriptAsync(createClientUsingScriptDto, totalCount);
            }
        }

        public async Task CreateScriptPostStatus(string scriptName, SeedingContentListDto seedingContentListDto, List<Guid> listScripIdtDto)
        {
            for (var i = 0; i < seedingContentListDto.SeedingContentDtos.Count; i++)
            {
                CreateUpdateScriptDto createUpdateScriptDto = new()
                {
                    ScriptName = $"{scriptName}+{i}",
                    IsActive = true,
                    IsDefault = false,
                    Type = Type.Type.PostStatus,
                    Value = $"{seedingContentListDto.SeedingContentDtos[i].Content}{_configuration.GetSection("FacebookParameter")["UrlCharacter"]}{seedingContentListDto.SeedingContentDtos[i].ImageUrl}"
                };

                var script = await _scriptAppService.CreateAsync(createUpdateScriptDto);

                listScripIdtDto.Add(script.Id);
            }
        }

        public async Task CreateScriptPostGroup(string scriptName, SeedingContentListDto seedingContentListDto, List<Guid> listScripIdtDto, Guid groupTypeId)
        {
            var groups = await _groupJoinRepository.GetListAsync(x => x.GroupTypeId == groupTypeId);
            var groupsUrl = groups.Select(x => x.GroupURL).Distinct().ToList();

            foreach (var groupUrl in groupsUrl)
            {
                for (var i = 0; i < seedingContentListDto.SeedingContentDtos.Count; i++)
                {
                    CreateUpdateScriptDto createUpdateScriptDto = new()
                    {
                        ScriptName = $"{scriptName}+{i}",
                        IsActive = true,
                        IsDefault = false,
                        Type = Type.Type.PostGroup,
                        Value = $"{groupUrl}{_configuration.GetSection("FacebookParameter")["UrlCharacter"]}{seedingContentListDto.SeedingContentDtos[i].Content}{_configuration.GetSection("FacebookParameter")["UrlCharacter"]}{seedingContentListDto.SeedingContentDtos[i].ImageUrl}"
                    };

                    var script = await _scriptAppService.CreateAsync(createUpdateScriptDto);

                    listScripIdtDto.Add(script.Id);
                }
            }
        }

        public async Task SheduleCreateClientUsingScriptAsync(CreateClientUsingScriptDto createClientUsingScriptDto, int totalCount)
        {
            int timeRandom = _randomAppService.RandomTime(totalCount);

            var clientScheduleLastJob = HangfireAppService.ClientAtScheduleJob(createClientUsingScriptDto.ClientId);

            var jobId = _backgroundJobClient.Schedule<IClientUsingScriptAppService>(x => x.CreateAsync(createClientUsingScriptDto),
                !clientScheduleLastJob.Item1
                ? TimeSpan.FromSeconds(_randomAppService.RandomInt(60, timeRandom * 60 + 60))
                : TimeSpan.FromSeconds(clientScheduleLastJob.Item2.Value.Second + _randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:ScheduleNewScript:Min"]), int.Parse(_configuration["TimeSchedule:ScheduleNewScript:Max"]))));

            CreateHangfireJobDto createHangfireJobDto = new()
            {
                ClientId = createClientUsingScriptDto.ClientId,
                ScriptId = createClientUsingScriptDto.ScriptId,
                JobId = Guid.Parse(jobId)
            };

            await _hangfireJobAppService.CreateAsync(createHangfireJobDto);
        }

        public async Task CommentPost(string seedingName, string url, string userName, int delayTime)
        {
            var seeding = await _seedingRepository.GetAsync(x => seedingName.Contains(x.Name));

            if (seeding.Comments > 0)
            {
                Task.Delay(delayTime).Wait();
                var commentContents = await _seedingContentCommentRepository.GetListAsync(x => x.SeedingId == seeding.Id);
                int commentedCount = 0;

                if (seeding.Comments < commentContents.Count)
                {
                    for (int i = 0; i < seeding.Comments; i++)
                    {
                        var randomIndexComment = _randomAppService.RandomInt(0, commentContents.Count);

                        CreateUpdateScriptDto createUpdateScriptDto = new()
                        {
                            ScriptName = $"Comment:+{seeding.Name}+{url}+{userName}+{i}",
                            IsActive = true,
                            IsDefault = false,
                            Type = Type.Type.CommentPost,
                            Value = $"{url}{_configuration.GetSection("FacebookParameter")["UrlCharacter"]}{commentContents[randomIndexComment].Content}",
                            ExtraSeedingCount = 1,
                            Username = userName
                        };

                        if (!HangfireAppService.IsAnyCreateScriptSchedule().Item1)
                        {
                            _backgroundJobClient.Schedule<IScriptAppService>(x => x.CreateAsync(createUpdateScriptDto),
                            TimeSpan.FromSeconds(_randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:CreateNewScript:Min"]), int.Parse(_configuration["TimeSchedule:CreateNewScript:Max"]))));
                        }
                        else
                        {
                            _backgroundJobClient.Schedule<IScriptAppService>(x => x.CreateAsync(createUpdateScriptDto),
                            TimeSpan.FromSeconds(HangfireAppService.IsAnyCreateScriptSchedule().Item2.Value.Second + _randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:CreateNewScript:Min"]), int.Parse(_configuration["TimeSchedule:CreateNewScript:Max"]))));
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < commentContents.Count; i++)
                    {
                        int randomClientCommentCount = 0;

                        if (i == commentContents.Count - 1)
                        {
                            randomClientCommentCount = seeding.Comments - commentedCount;
                        }
                        else
                        {
                            var commentPerClientCount = Math.Ceiling((decimal)seeding.Comments / (decimal)commentContents.Count);

                            randomClientCommentCount = _randomAppService.RandomInt(1, (int)commentPerClientCount);
                        }

                        CreateUpdateScriptDto createUpdateScriptDto = new()
                        {
                            ScriptName = $"Comment:+{seeding.Name}+{url}+{userName}+{i}",
                            IsActive = true,
                            IsDefault = false,
                            Type = Type.Type.CommentPost,
                            Value = $"{url}{_configuration.GetSection("FacebookParameter")["UrlCharacter"]}{commentContents[i].Content}",
                            ExtraSeedingCount = randomClientCommentCount,
                            Username = userName
                        };

                        if (!HangfireAppService.IsAnyCreateScriptSchedule().Item1)
                        {
                            _backgroundJobClient.Schedule<IScriptAppService>(x => x.CreateAsync(createUpdateScriptDto),
                            TimeSpan.FromSeconds(_randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:CreateNewScript:Min"]), int.Parse(_configuration["TimeSchedule:CreateNewScript:Max"]))));
                        }
                        else
                        {
                            _backgroundJobClient.Schedule<IScriptAppService>(x => x.CreateAsync(createUpdateScriptDto),
                            TimeSpan.FromSeconds(HangfireAppService.IsAnyCreateScriptSchedule().Item2.Value.Second + _randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:CreateNewScript:Min"]), int.Parse(_configuration["TimeSchedule:CreateNewScript:Max"]))));
                        }

                        commentedCount += randomClientCommentCount;
                    }
                }
            }
        }

        public async Task SharePost(string seedingName, string url, string userName, int delayTime)
        {
            var seeding = await _seedingRepository.GetAsync(x => seedingName.Contains(x.Name));

            if (seeding.SharesWall > 0)
            {
                Task.Delay(delayTime).Wait();
                var shareContents = await _seedingContentShareRepository.GetListAsync(x => x.SeedingId == seeding.Id);
                int sharedCount = 0;

                if (seeding.SharesWall < shareContents.Count)
                {
                    for (int i = 0; i < seeding.SharesWall; i++)
                    {
                        var randomIndexShare = _randomAppService.RandomInt(0, shareContents.Count);

                        CreateUpdateScriptDto createUpdateScriptDto = new()
                        {
                            ScriptName = $"ShareWall:+{seeding.Name}+{url}+{userName}+{i}",
                            IsActive = true,
                            IsDefault = false,
                            Type = Type.Type.SharePost,
                            Value = $"{url}{_configuration.GetSection("FacebookParameter")["UrlCharacter"]}{shareContents[randomIndexShare].Content}",
                            ExtraSeedingCount = 1,
                            Username = userName
                        };

                        if (!HangfireAppService.IsAnyCreateScriptSchedule().Item1)
                        {
                            _backgroundJobClient.Schedule<IScriptAppService>(x => x.CreateAsync(createUpdateScriptDto),
                            TimeSpan.FromSeconds(_randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:CreateNewScript:Min"]), int.Parse(_configuration["TimeSchedule:CreateNewScript:Max"]))));
                        }
                        else
                        {
                            _backgroundJobClient.Schedule<IScriptAppService>(x => x.CreateAsync(createUpdateScriptDto),
                            TimeSpan.FromSeconds(HangfireAppService.IsAnyCreateScriptSchedule().Item2.Value.Second + _randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:CreateNewScript:Min"]), int.Parse(_configuration["TimeSchedule:CreateNewScript:Max"]))));
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < shareContents.Count; i++)
                    {
                        int randomClientShareCount = 0;

                        if (i == shareContents.Count - 1)
                        {
                            randomClientShareCount = seeding.SharesWall - sharedCount;
                        }
                        else
                        {
                            var sharePerClientCount = Math.Ceiling((decimal)seeding.SharesWall / (decimal)shareContents.Count);

                            randomClientShareCount = _randomAppService.RandomInt(1, (int)sharePerClientCount);
                        }

                        CreateUpdateScriptDto createUpdateScriptDto = new()
                        {
                            ScriptName = $"ShareWall:+{seeding.Name}+{url}+{userName}+{i}",
                            IsActive = true,
                            IsDefault = false,
                            Type = Type.Type.SharePost,
                            Value = $"{url}{_configuration.GetSection("FacebookParameter")["UrlCharacter"]}{shareContents[i].Content}",
                            ExtraSeedingCount = randomClientShareCount,
                            Username = userName
                        };

                        if (!HangfireAppService.IsAnyCreateScriptSchedule().Item1)
                        {
                            _backgroundJobClient.Schedule<IScriptAppService>(x => x.CreateAsync(createUpdateScriptDto),
                            TimeSpan.FromSeconds(_randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:CreateNewScript:Min"]), int.Parse(_configuration["TimeSchedule:CreateNewScript:Max"]))));
                        }
                        else
                        {
                            _backgroundJobClient.Schedule<IScriptAppService>(x => x.CreateAsync(createUpdateScriptDto),
                            TimeSpan.FromSeconds(HangfireAppService.IsAnyCreateScriptSchedule().Item2.Value.Second + _randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:CreateNewScript:Min"]), int.Parse(_configuration["TimeSchedule:CreateNewScript:Max"]))));
                        }

                        sharedCount += randomClientShareCount;
                    }
                }
            }
        }

        public async Task ReactPost(string seedingName, string url, string userName, int delayTime)
        {
            var seeding = await _seedingRepository.GetAsync(x => seedingName.Contains(x.Name));

            if (seeding.Reacts > 0)
            {
                Task.Delay(delayTime).Wait();
                var reactions = new List<string> { "Thích", "Yêu thích", "Thương thương" };
                int clientReactedCount = seeding.Reacts;

                for (int i = 0; i < reactions.Count; i++)
                {
                    int randomClientReactCount = 0;

                    if (i == reactions.Count - 1)
                    {
                        randomClientReactCount = clientReactedCount;
                    }
                    else
                    {
                        randomClientReactCount = _randomAppService.RandomInt(0, clientReactedCount);
                    }

                    if (randomClientReactCount > 0 && clientReactedCount > 0)
                    {
                        CreateUpdateScriptDto createUpdateScriptDto = new()
                        {
                            ScriptName = $"Reacts:+{seeding.Name}+{url}+{userName}+{reactions[i]}",
                            IsActive = true,
                            IsDefault = false,
                            Type = Type.Type.ReactionPost,
                            Value = $"{url}{_configuration.GetSection("FacebookParameter")["UrlCharacter"]}{randomClientReactCount}{_configuration.GetSection("FacebookParameter")["UrlCharacter"]}{reactions[i]}",
                            Username = userName
                        };

                        if (!HangfireAppService.IsAnyCreateScriptSchedule().Item1)
                        {
                            _backgroundJobClient.Schedule<IScriptAppService>(x => x.CreateAsync(createUpdateScriptDto),
                            TimeSpan.FromSeconds(_randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:CreateNewScript:Min"]), int.Parse(_configuration["TimeSchedule:CreateNewScript:Max"]))));
                        }
                        else
                        {
                            _backgroundJobClient.Schedule<IScriptAppService>(x => x.CreateAsync(createUpdateScriptDto),
                            TimeSpan.FromSeconds(HangfireAppService.IsAnyCreateScriptSchedule().Item2.Value.Second + _randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:CreateNewScript:Min"]), int.Parse(_configuration["TimeSchedule:CreateNewScript:Max"]))));
                        }
                    }

                    clientReactedCount -= randomClientReactCount;
                }
            }
        }

        public async Task SharePostToGroup(string seedingName, string url, string userName, int delayTime)
        {
            var seeding = await _seedingRepository.GetAsync(x => seedingName.Contains(x.Name));

            if (seeding.SharesGroup > 0)
            {
                Task.Delay(delayTime).Wait();
                var listGroup = await _groupJoinRepository.GetListAsync(x => x.GroupTypeId == seeding.GroupTypeId);
                var listGroupSelectName = listGroup.Select(x => x.GroupName).Distinct().ToList();

                for (var i = 0; i < listGroupSelectName.Count; i++)
                {
                    CreateUpdateScriptDto createUpdateScriptDto = new()
                    {
                        ScriptName = $"ShareGroup:+{seeding.Name}+{url}+{userName}+{i}",
                        IsActive = true,
                        IsDefault = false,
                        Type = Type.Type.SharePostToGroup,
                        Value = $"{url}{_configuration.GetSection("FacebookParameter")["UrlCharacter"]}{listGroupSelectName[i]}",
                        ExtraSeedingCount = 1,
                        Username = userName,
                        GroupTypeId = seeding.GroupTypeId
                    };

                    if (!HangfireAppService.IsAnyCreateScriptSchedule().Item1)
                    {
                        _backgroundJobClient.Schedule<IScriptAppService>(x => x.CreateAsync(createUpdateScriptDto),
                        TimeSpan.FromSeconds(_randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:CreateNewScript:Min"]), int.Parse(_configuration["TimeSchedule:CreateNewScript:Max"]))));
                    }
                    else
                    {
                        _backgroundJobClient.Schedule<IScriptAppService>(x => x.CreateAsync(createUpdateScriptDto),
                        TimeSpan.FromSeconds(HangfireAppService.IsAnyCreateScriptSchedule().Item2.Value.Second + _randomAppService.RandomInt(int.Parse(_configuration["TimeSchedule:CreateNewScript:Min"]), int.Parse(_configuration["TimeSchedule:CreateNewScript:Max"]))));
                    }
                }
            }
        }


        public async Task<PagedResultDto<SeedingDto>> GetListSeeding(string name, int skip, int take, int conditionValue)
        {
            if (name.IsNullOrWhiteSpace() || name == "null")
            {
                name = "";
            }
            var listSeeeding = await _seedingRepository.GetListAsync(x => x.Name.Contains(name));
            var totalCount = listSeeeding.Count;

            listSeeeding = listSeeeding
                .WhereIf(conditionValue == 1, seeding => _scriptRepository.Any(script => script.ScriptName.Contains(seeding.Name) && script.IsActive
                && _clientUsingScriptsRepository.Any(cus => cus.ScriptId == script.Id && cus.IsActive
                && _clientRepository.Any(client => client.Id == cus.ClientId && client.IsActive && !client.HasCheckPoint))))
                .WhereIf(conditionValue == 2, seeding => _scriptRepository.Any(script => script.ScriptName.Contains(seeding.Name) && script.IsActive
                && _clientUsingScriptsRepository.Any(cus => cus.ScriptId == script.Id && !cus.IsActive
                && _clientRepository.Any(client => client.Id == cus.ClientId && client.IsActive && !client.HasCheckPoint))))
                .Skip(skip)
                .Take(take)
                .OrderByDescending(x => x.CreationTime)
                .ToList();
            var listSeedingDto = ObjectMapper.Map<List<Seeding>, List<SeedingDto>>(listSeeeding);

            listSeedingDto.ForEach(x =>
            {
                var listSeedingContent = _seedingContentRepository.Where(sc => sc.SeedingId == x.Id).ToList();
                var listScript = _scriptRepository.Where(sc => sc.ScriptName.Contains(x.Name)).ToList();
                var reacts = _hangfireJobRepository.Where(hf => hf.IsFinish && _scriptRepository.Any(sc => sc.ScriptName.Contains("Reacts:+" + x.Name) && sc.Id == hf.ScriptId)).ToList();
                x.CountReacts = reacts.Count + "/" + x.Reacts;
                var comments = _hangfireJobRepository.Where(hf => hf.IsFinish && _scriptRepository.Any(sc => sc.ScriptName.Contains("Comment:+" + x.Name) && sc.Id == hf.ScriptId)).ToList();
                x.CountComments = comments.Count + "/" + x.Reacts;
                var postWall = _hangfireJobRepository.Where(hf => hf.IsFinish && _scriptRepository.Any(sc => sc.ScriptName.Contains("Post Wall:+" + x.Name) && sc.Id == hf.ScriptId)).ToList();
                x.CountPostWall = postWall.Count + "/" + x.Reacts;
                var postGroups = _hangfireJobRepository.Where(hf => hf.IsFinish && _scriptRepository.Any(sc => sc.ScriptName.Contains("Post Group:+" + x.Name) && sc.Id == hf.ScriptId)).ToList();
                x.CountPostGroups = postGroups.Count + "/" + x.Reacts;
                var shareWall = _hangfireJobRepository.Where(hf => hf.IsFinish && _scriptRepository.Any(sc => sc.ScriptName.Contains("ShareWall:+" + x.Name) && sc.Id == hf.ScriptId)).ToList();
                x.CountShareWall = shareWall.Count + "/" + x.Reacts;
                var shareGroup = _hangfireJobRepository.Where(hf => hf.IsFinish && _scriptRepository.Any(sc => sc.ScriptName.Contains("ShareGroup:+" + x.Name) && sc.Id == hf.ScriptId)).ToList();
                x.CountShareGroups = shareGroup.Count + "/" + x.Reacts;
                x.Processed = (reacts.Count + comments.Count + postWall.Count + postGroups.Count + shareWall.Count + shareGroup.Count)
                + "/" + (x.Reacts + x.Comments + x.PostsWall + x.PostsGroup + x.SharesWall + x.SharesGroup);
                //x.GroupName = EnumAppService.GetNameEnum<GroupTypeId>(x.GroupType);
                x.GroupName = _groupTypeRepository.FirstOrDefault(gt => gt.Id == x.GroupTypeId).Name;
                x.IsFinish = _clientActivitiesRepository.Count(c => c.ScriptName == x.Name) >= (x.PostsGroup + x.PostsWall + x.Comments + x.SharesGroup + x.SharesGroup + x.Reacts);
                x.SeedingContentDtos = ObjectMapper.Map<List<SeedingContents.SeedingContent>, List<SeedingContentDto>>(listSeedingContent);
            });
            return new PagedResultDto<SeedingDto>
            {
                TotalCount = totalCount,
                Items = listSeedingDto
            };
        }

        public async Task<SeedingBackgroundJobDto> ListClientScheduleSeeding(string seedingName)
        {
            var jobStorage = JobStorage.Current.GetMonitoringApi();

            var clientsUsingScriptSeeding = await _clientRepository.GetListAsync(client => _clientUsingScriptsRepository.Any(cus => cus.ClientId == client.Id
            && _scriptRepository.Any(script => script.Id == cus.ScriptId && _seedingRepository.Any(s => script.ScriptName.Contains(s.Name) && s.Name.Contains(seedingName)))));

            var clientsSeedingMap = ObjectMapper.Map<List<Clients.Client>, List<ClientSeedingDto>>(clientsUsingScriptSeeding);

            clientsSeedingMap.ForEach(x =>
            {
                var script = _scriptRepository.FirstOrDefault(s => s.ScriptName.Contains(seedingName));
                var clientUsingScript = _clientUsingScriptsRepository.FirstOrDefault(c => c.ScriptId == script.Id && c.IsActive && _clientRepository.Any(client => client.Id == x.Id));
                var clientAtScheduleJob = HangfireAppService.ClientAtScheduleJob(x.Id);

                x.ScriptName = script.ScriptName;
                x.ScriptId = script.Id;
                x.IsActive = clientUsingScript != null && clientUsingScript.IsActive;
                x.Value = script.Value;
                x.EnqueueAt = clientAtScheduleJob.Item2;
            });

            return new SeedingBackgroundJobDto()
            {
                ClientSeedingDtos = clientsSeedingMap
            };
        }
        public async Task DeleteAsync(Guid id)
        {
            var seedingContents = await _seedingContentRepository.GetListAsync(x => x.SeedingId == id);
            var seedingComments = await _seedingContentCommentRepository.GetListAsync(x => x.SeedingId == id);
            var seedingShares = await _seedingContentShareRepository.GetListAsync(x => x.SeedingId == id);

            await _seedingContentRepository.DeleteManyAsync(seedingContents);
            await _seedingContentCommentRepository.DeleteManyAsync(seedingComments);
            await _seedingContentShareRepository.DeleteManyAsync(seedingShares);

            await _seedingRepository.DeleteAsync(id);
        }
        public PagedResultDto<ClientSeedingJobDto> GetListClientSeedingJob(string seedingName, string clientId, int skip, int take)
        {
            var query = from client in _clientRepository
                        join job in _hangfireJobRepository on client.Id equals job.ClientId
                        join script in _scriptRepository on job.ScriptId equals script.Id
                        orderby client.NameFacebook descending
                        where seedingName != "" ? script.ScriptName.Contains(seedingName) : false || clientId == "" || client.UserName == clientId
                        select new ClientSeedingJobDto()
                        {
                            ClientId = client.UserName,
                            ClientName = client.NameFacebook,
                            TypeSeeding = EnumAppService.GetNameEnum<Type.Type>(script.Type),
                            Value = script.Value,
                            Status = job.IsFinish,
                            CreationTime = job.CreationTime
                        };

            var result = query.Skip(skip).Take(take).ToList();
            return new PagedResultDto<ClientSeedingJobDto>(query.Count(), result);

        }

        public PagedResultDto<DashBoardSeedingDto> GetListSeedingByClientAsync(string clientId, int skip, int take)
        {

            var query = from client in _clientRepository
                        join hangFire in _hangfireJobRepository on client.Id equals hangFire.ClientId
                        join script in _scriptRepository on hangFire.ScriptId equals script.Id
                        orderby client.NameFacebook descending
                        where client.UserName == clientId && _seedingRepository.Any(seeding => script.ScriptName.Contains(seeding.Name))
                        select new DashBoardSeedingDto()
                        {
                            InfoSeeding = script.ScriptName,
                            Value = script.Value,
                            TypeSeeding = EnumAppService.GetNameEnum<Type.Type>(script.Type),
                            StatusSeeding = hangFire.IsFinish

                        };
            var result = query.Skip(skip).Take(take).ToList();
            return new PagedResultDto<DashBoardSeedingDto>(query.Count(), result);


        }
        public List<string> GetListNameSeeding()
        {
            var listName = _seedingRepository.Select(x => x.Name).ToList();
            return listName;
        }
    }
}

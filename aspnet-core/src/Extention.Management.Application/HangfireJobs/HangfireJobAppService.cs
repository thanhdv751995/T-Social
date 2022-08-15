using Extention.Management.ClientActivities;
using Extention.Management.Clients;
using Extention.Management.Enums;
using Extention.Management.HangfireJob;
using Extention.Management.HangfireJobs;
using Extention.Management.Profiles;
using Extention.Management.Proxys;
using Extention.Management.Scripts;
using Extention.Management.Seedings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Uow;

namespace Extention.Management
{
    public class HangfireJobAppService : ManagementAppService, IHangfireJobAppService
    {
        private readonly IHangfireJobRepository _hangfireJobRepository;
        private readonly HangfireJobManager _hangfireJobManager;
        private readonly IScriptRepository _scriptRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ISeedingRepository _seedingRepository;
        private readonly IProxyRepository _proxyRepository;
        private readonly IProfileClientRepository _profileRepository;
        private readonly IClientActivityRepository _clientActivityRepository;
        public HangfireJobAppService(
            IHangfireJobRepository hangfireJobRepository,
            HangfireJobManager hangfireJobManager,
            IScriptRepository scriptRepository,
            IClientRepository clientRepository,
            ISeedingRepository seedingRepository,
            IProxyRepository proxyRepository,
            IProfileClientRepository profileRepository,
            IClientActivityRepository clientActivityRepository
            )
        {
            _hangfireJobRepository = hangfireJobRepository;
            _hangfireJobManager = hangfireJobManager;
            _scriptRepository = scriptRepository;
            _clientRepository = clientRepository;
            _seedingRepository = seedingRepository;
            _proxyRepository = proxyRepository;
            _profileRepository = profileRepository;
            _clientActivityRepository = clientActivityRepository;
        }
        [UnitOfWork]
        public async Task CreateAsync(CreateHangfireJobDto createHangfireJobDto)
        {
            var hangfireJob = _hangfireJobManager.CreateAsync(createHangfireJobDto.JobId, createHangfireJobDto.ClientId, createHangfireJobDto.ScriptId, false);

            await _hangfireJobRepository.InsertAsync(hangfireJob);
        }
        [UnitOfWork]
        public async Task UpdateAsync(Guid clientId, Guid scriptId)
        {
            var hangfireJob = _hangfireJobRepository.FirstOrDefault(x => x.ClientId == clientId && x.ScriptId == scriptId && !x.IsFinish);

            if (hangfireJob != null)
            {
                hangfireJob.IsFinish = true;
                await _hangfireJobRepository.UpdateAsync(hangfireJob);
            }
        }

        public async Task<PagedResultDto<HangfireJobDto>> GetHangfireJobs(PagedResultRequestDto pagedResultRequestDto, string seedingName,int isFinish, Guid? clientId, int minuteLate)
        {
            var currentTime = DateTime.Now;
            List<HangfireJobs.HangfireJob> hangfireJobs = new();

            hangfireJobs = await _hangfireJobRepository.GetListAsync(x => _scriptRepository.Any(s => s.Id == x.ScriptId && s.ScriptName.Contains(seedingName)));

            var hangfireJobsMapDto = ObjectMapper.Map<List<HangfireJobs.HangfireJob>, List<HangfireJobDto>>(hangfireJobs);

            hangfireJobsMapDto = hangfireJobsMapDto
                    .WhereIf(isFinish == 1, x => x.IsFinish)
                    .WhereIf(isFinish == 2, x => !x.IsFinish)
                    .WhereIf(clientId.HasValue, x => x.ClientId == clientId)
                    .WhereIf(minuteLate != 0, x => x.CreationTime.AddMinutes(minuteLate) < currentTime && !x.IsFinish).ToList();
            
            var result = hangfireJobsMapDto.Skip(pagedResultRequestDto.SkipCount).Take(pagedResultRequestDto.MaxResultCount).ToList();

            result.ForEach(x =>
            {
                var script = _scriptRepository.FirstOrDefault(s => s.Id == x.ScriptId);
                var client = _clientRepository.FirstOrDefault(c => c.Id == x.ClientId);

                x.ClientId = client.Id;
                x.Username = client.UserName;
                x.NameFacebook = client.NameFacebook;
                x.ScriptId = script.Id;
                x.ScriptName = script.ScriptName;
                x.Value = script.Value;
                x.Type = script.Type;
                x.TypeName = EnumAppService.GetNameEnum<Type.Type>(script.Type);
                x.MinuteLate = !x.IsFinish ? (int)Math.Ceiling((currentTime - x.CreationTime).TotalMinutes) - 10 : 0;

            });   
            return new PagedResultDto<HangfireJobDto>(
                       hangfireJobsMapDto.Count,
                       result);
        }
        public async Task<PagedResultDto<HangfireJobDto>> GetHangfireJobsUnfinished(PagedResultRequestDto pagedResultRequestDto, string seedingName, Guid? clientId)
        {
            var isFinish = 0;
            var currentTime = DateTime.Now;
            var hangfireJobs = await _hangfireJobRepository.GetListAsync(x => _scriptRepository.Any(s => s.Id == x.ScriptId && s.ScriptName.Contains(seedingName)));

            var hangfireJobsMapDto = ObjectMapper.Map<List<HangfireJobs.HangfireJob>, List<HangfireJobDto>>(hangfireJobs);

            hangfireJobsMapDto = hangfireJobsMapDto
                .WhereIf(isFinish == 0, x => !x.IsFinish)
                .WhereIf(clientId.HasValue, x => x.ClientId == clientId)
                .Skip(pagedResultRequestDto.SkipCount).Take(pagedResultRequestDto.MaxResultCount).ToList();

            hangfireJobsMapDto.ForEach(x =>
            {
                var script = _scriptRepository.FirstOrDefault(s => s.Id == x.ScriptId);
                var client = _clientRepository.FirstOrDefault(c => c.Id == x.ClientId);

                x.ClientId = client.Id;
                x.Username = client.UserName;
                x.NameFacebook = client.NameFacebook;
                x.ScriptId = script.Id;
                x.ScriptName = script.ScriptName;
                x.Value = script.Value;
                x.Type = script.Type;
                x.TypeName = EnumAppService.GetNameEnum<Type.Type>(script.Type);
                x.MinuteLate = !x.IsFinish ? (int)Math.Ceiling((currentTime - x.CreationTime).TotalMinutes) - 10 : 0;

            });
            return new PagedResultDto<HangfireJobDto>(
                       hangfireJobsMapDto.Count,
                       hangfireJobsMapDto);
        }
        public async Task<List<int>> GetJobForChart(DateTime? startDate, DateTime? endDate)
        {
            DateTime localDate = DateTime.Now.AddDays(-6);
            List<ClientActivity> listActivity = new();
            if (startDate != null && endDate != null)
            {
                listActivity = await _clientActivityRepository.GetListAsync(x => x.CreationTime >= startDate && x.CreationTime <= endDate);
            }
            else
            {
                listActivity = await _clientActivityRepository.GetListAsync(x => x.CreationTime >= localDate);
            }
            //var listSeedingName = _seedingRepository.Select(x => x.Name).ToList();
            //var listScript = listActivity.Where(x => listSeedingName.Any(s => x.ScriptName.Contains(s)));
            var listScript = listActivity.Where(x => x.ScriptName.Contains(": "));
            List<int> data = new(new int[] { 0, 0, 0 });
            if (startDate != null && endDate != null)
            {
                data[0] = _clientActivityRepository.Count(x => x.CreationTime >= startDate && x.CreationTime <= endDate);
            }
            else
            {
                data[0] = _clientActivityRepository.Count(x => x.CreationTime >= localDate);
            }
            data[1] = listScript.Count();
            data[2] = data[0] - data[1];
            return data;
        }
        public List<IngredientDto> GetLineChart()
        {
            List<IngredientDto> ingredientDtos = new();
            List<int> data = new(new int[] { 0, 0, 0, 0, 0 })
            {
                [0] = _clientRepository.Count(),
                [1] = _proxyRepository.Count(),
                [2] = _seedingRepository.Count(),
                [3] = _profileRepository.Count(),
                [4] = _scriptRepository.Count()
            };
            List<string> listName = new(new string[] { "Tài khoản Facebook", "Proxy", "Chiến dịch", "Profile", "Kịch bản" });
            for (int i = 0; i < listName.Count; i++)
            {
                IngredientDto ingredientDto = new();
                ingredientDto.Ingredient = listName[i];
                ingredientDto.Total = data[i];
                ingredientDtos.Add(ingredientDto);
            }

            return ingredientDtos;
        }
    }
}

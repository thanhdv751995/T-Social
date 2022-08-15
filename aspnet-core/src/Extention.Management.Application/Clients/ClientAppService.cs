using ExtensionsManagement.ClientFacebookDtos;
using ExtensionsManagement.ClientFacebooks;
using Extention.Management.AccountUsingScripts;
using Extention.Management.BackgroundJob;
using Extention.Management.ChromeProfile;
using Extention.Management.Client;
using Extention.Management.ClientActivities;
using Extention.Management.ClientBelongToProfiles;
using Extention.Management.ClientFriends;
using Extention.Management.ClientInfomations;
using Extention.Management.Clients.Entity;
using Extention.Management.Commenteds;
using Extention.Management.Enums;
using Extention.Management.HangfireJobs;
using Extention.Management.Histories;
using Extention.Management.Profiles;
using Extention.Management.Proxys;
using Extention.Management.ProxyUsingScript;
using Extention.Management.Scripts;
using Extention.Management.VirtualMachines;
using Hangfire;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace Extention.Management.Clients
{
    public class ClientAppService : ManagementAppService, IClientAppService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ClientManager _clientManager;
        private readonly CProfileAppService _cProfileAppService;
        private readonly IClientInfomationRepository _clientInfomationRepository;
        private readonly IClientActivityRepository _clientActivityRepository;
        private readonly IClientFriendRepository _clientFriendRepository;
        private readonly IHistoryRepository _historiesRepository;
        private readonly IClientUsingScriptRepository _clientUsingScriptsRepository;
        private readonly IScriptRepository _scriptRepository;
        private readonly ClientActivityAppService _clientActivityAppService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IProfileClientRepository _profileClientRepository;
        private readonly VirtualMachineAppService _virtualMachineAppService;
        private readonly IClientBelongToProfileRepository _clientBeLongToProfileRepository;
        private readonly IHangfireJobRepository _hangfireJobRepository;
        public ClientAppService(IClientRepository clientRepository,
          ClientManager clientManager,
          CProfileAppService cProfileAppService,
          IClientInfomationRepository clientInfomationRepository,
          IClientActivityRepository clientActivityRepository,
          IClientFriendRepository clientFriendRepository,
          IHistoryRepository historiesRepository,
          IClientUsingScriptRepository clientUsingScriptsRepository,
          IScriptRepository scriptRepository,
          ClientActivityAppService clientActivityAppService,
          IUnitOfWorkManager unitOfWorkManager,
          IProfileClientRepository profileClientRepository,
          VirtualMachineAppService virtualMachineAppService,
          IClientBelongToProfileRepository clientBeLongToProfileRepository,
          IHangfireJobRepository hangfireJobRepository)
        {
            _clientRepository = clientRepository;
            _clientManager = clientManager;
            _cProfileAppService = cProfileAppService;
            _clientActivityRepository = clientActivityRepository;
            _clientInfomationRepository = clientInfomationRepository;
            _clientFriendRepository = clientFriendRepository;
            _historiesRepository = historiesRepository;
            _clientUsingScriptsRepository = clientUsingScriptsRepository;
            _scriptRepository = scriptRepository;
            _profileClientRepository = profileClientRepository;
            _clientActivityAppService = clientActivityAppService;
            _unitOfWorkManager = unitOfWorkManager;
            _virtualMachineAppService = virtualMachineAppService;
            _clientBeLongToProfileRepository = clientBeLongToProfileRepository;
            _hangfireJobRepository = hangfireJobRepository;
        }
        public async Task<ClientDto> GetAsync(Guid id)
        {
            var client = await _clientRepository.GetAsync(id);
            return ObjectMapper.Map<Client, ClientDto>(client);
        }
        public async Task<ClientDto> CreateAsync(CreateUpdateClientDto input)
        {
            var client = _clientManager.CreateAsync(input.NameFacebook, input.AvatarUrl, input.UserName, input.Password, input.SecretKey, input.Cookie, false,
                string.Empty, input.AccessToken, string.Empty, string.Empty, false, false);

            await _clientRepository.InsertAsync(client);
            return ObjectMapper.Map<Client, ClientDto>(client);
        }
        public async Task CreateWithExcel(List<AddAcountWithExcelDto> input)
        {
            List<Client> listClient = new();
            foreach(var accout in input)
            {
                var client = _clientManager.CreateAsync(accout.NameFacebook, accout.AvatarUrl, accout.UserName, accout.Password, accout.SecretKey, string.Empty, false,
                string.Empty, string.Empty, string.Empty, string.Empty, false, false);
                listClient.Add(client);
            }    
            await _clientRepository.InsertManyAsync(listClient);
        }
        // lấy danh sánh client từ id kịch bản đang chạy
        public async Task<List<ClientDto>> GetListClientFromIdScript(Guid scriptId)
        {
            var listCL = _clientUsingScriptsRepository.GetListAsync(x => x.ScriptId == scriptId );
            var clients = new List<Client>();
            for (int i =0;i< listCL.Result.Count; i++)
            {
                var client = await _clientRepository.GetAsync(listCL.Result[i].ClientId);
                clients.Add(client);
            }

            var clientsMap = ObjectMapper.Map<List<Client>, List<ClientDto>>(clients);
            clientsMap.ForEach(client =>
            {
                client.IsActiveScript = _clientUsingScriptsRepository.Any(x => x.ClientId == client.Id && x.IsActive);
            });
            return clientsMap;
        }
        public async Task<PagedResultDto<ClientDto>> GetListAsync(GetClientListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Client.UserName);
            }

            var clients = await _clientRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );
            var totalCount = input.Filter == null
                ? await _clientRepository.CountAsync()
                : await _clientRepository.CountAsync(
                    client => client.UserName.Contains(input.Filter) || client.NameFacebook.Contains(input.Filter));

            return new PagedResultDto<ClientDto>(
                totalCount,
                ObjectMapper.Map<List<Client>, List<ClientDto>>(clients)
            );
        }

        public async Task<List<ClientDto>> GetListClientAsync()
        {
            var clients = await _clientRepository.GetListAsync(x => _historiesRepository.Any(h => h.ClientId == x.Id));
            return ObjectMapper.Map<List<Client>, List<ClientDto>>(clients);
        }


        public async Task UpdateAsync(Guid id, CreateUpdateClientDto input)
        {
            var client = await _clientRepository.GetAsync(id);

            client.UserName = input.UserName;
            client.NameFacebook = input.NameFacebook;
            client.SecretKey = input.SecretKey;
            client.Cookie = input.Cookie;
            client.Password = input.Password;
            client.IsActive = input.IsActive;
            client.ProxyIp = input.ProxyIp;
            client.ChromeProfile = input.ChromeProfile;
            client.AccessToken = input.AccessToken;
            client.ComputerName = input.ComputerName;
            await _clientRepository.UpdateAsync(client);
        }
        public async Task UpdateOnline(UpdateOnlineDto input)
        {
            using var uow = _unitOfWorkManager.Begin(true);
            var client = await _clientRepository.GetAsync(input.ClientId);
            client.Online = input.isOnline;
            await _clientRepository.UpdateAsync(client);
            await uow.CompleteAsync();
        }

        public async Task UpdateClientOnline(UpdateClientOnlineDto updateClientOnlineDto)
        {
            var client = await _clientRepository.GetAsync(x => x.UserName == updateClientOnlineDto.Username);
            client.Online = updateClientOnlineDto.Online;

            await _clientRepository.UpdateAsync(client);
        }

        public async Task UpdateClientOnlineByChromeProfile(UpdateClientOnlineByChromeProfileDto updateClientOnlineByChromeProfileDto)
        {
            var client = await _clientRepository.GetAsync(x => x.ChromeProfile == updateClientOnlineByChromeProfileDto.ChromeProfile);
            client.Online = true;

            await _clientRepository.UpdateAsync(client);
        }

        public async Task<PagedResultDto<ClientDto>> GetListClientActive(string nameClient, string profileName , string proxyIp, int skip, int take)
        {
            if (nameClient.IsNullOrWhiteSpace() || nameClient == "null")
            {
                nameClient = "";
            }
            var nameProfile = "";

             var query = await _clientRepository.GetListClientActive(nameClient, profileName, proxyIp);
            var totalCount = query.Count;
            var clients = query.Distinct().Skip(skip).Take(take).ToList();

            var clientsMap = ObjectMapper.Map<List<Client>, List<ClientDto>>(clients);
            //var profile = await _profileClientRepository.FirstAsync(x => x.ProfileName == profileName);
            clientsMap.ForEach(x =>
            {
                var listProfileClient = _profileClientRepository.Where(c =>
                _clientBeLongToProfileRepository.Any(cbp => cbp.ClientId == x.Id && cbp.ProfileClientId == c.Id)).Select(c => c.ProfileName).Distinct().ToList();
                for(int i = 0;i < listProfileClient.Count; i++)
                {
                    if(i < listProfileClient.Count - 1)
                    {
                        nameProfile = nameProfile + listProfileClient[i] + ",";
                    }
                    else
                    {
                        nameProfile += listProfileClient[i];
                    }
                }
                    x.NameProfile = nameProfile;
                     nameProfile = "";
            });
            return new PagedResultDto<ClientDto>(totalCount,clientsMap);
        }
        public async Task UpdateIpProxy(string userName, string Ip)
        {
            var facebookAccount = await _clientRepository.GetAsync(x => x.UserName == userName);
            facebookAccount.IsActive = !facebookAccount.IsActive;
            facebookAccount.ProxyIp = Ip;
            await _clientRepository.UpdateAsync(facebookAccount);
        }
        public async Task<bool> UpdateActive(UpdateActiveDto updateActiveDto)
        {
            try
            {
                var facebookAccount = await _clientRepository.GetAsync(x => x.UserName == updateActiveDto.Username);

                facebookAccount.IsActive = true;

                facebookAccount.Online = true;

                facebookAccount.ProxyIp = updateActiveDto.IP;

                facebookAccount.ComputerName = _virtualMachineAppService.GetVM();

                if (_cProfileAppService.IsAnyCProfile())
                {
                    facebookAccount.ChromeProfile = string.Empty;
                }
                else
                {
                    facebookAccount.ChromeProfile = _cProfileAppService.GetCurrentCProfile().Profile;
                }

                //await _proxyAppService.UpdateActive(updateActiveDto.IP);
                //await UpdateIpProxy(userName, IP);
                await _clientRepository.UpdateAsync(facebookAccount);

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                CreateUpdateDto createUpdateDto = new()
                {
                    Profile = string.Empty
                };

                await _cProfileAppService.CreateUpdateCProfile(createUpdateDto);
            }
        }
        public async Task DeleteManyActivities(string userId)
        {
            var activities = await _clientActivityRepository.GetListAsync(x => x.UserName == userId);

            await _clientActivityRepository.DeleteManyAsync(activities);
        }

        public async Task DeleteManyFriends(Guid userId)
        {
            var friends = await _clientFriendRepository.GetListAsync(x => x.IdUser == userId);

            await _clientFriendRepository.DeleteManyAsync(friends);
        }
        public async Task DeleteManyHistory(Guid userId)
        {
            var history = await _historiesRepository.GetListAsync(x => x.ClientId == userId);

            await _historiesRepository.DeleteManyAsync(history);
        }
        public async Task DeleteInformationAsync(Guid userId)
        {
            var information = await _clientInfomationRepository.GetAsync(x => x.IdUser == userId);

            await _clientInfomationRepository.DeleteAsync(information);
        }
        public async Task DeleteManyAsync(Guid[] ids)
        {
            //try
            //{
            //   foreach(var id in ids)
            //    {
            //        var listFriend = await _clientFriendRepository.GetListAsync(x => x.IdUser == id);
            //        var listActivities = await _clientActivityRepository.GetListAsync(x => x.IdUser == id);
            //        var commented = await _commentedRepository.GetListAsync(x => x.ClientId == id);
            //        var histories = await _historiesRepository.GetListAsync(x => x.ClientId == id);

            //        if(_clientInfomationRepository.Any(x => x.IdUser == id))
            //        {
            //            var infomation = await _clientInfomationRepository.GetAsync(x => x.IdUser == id);
            //            await _clientInfomationRepository.DeleteAsync(infomation);
            //        }

            //        await _clientActivityRepository.DeleteManyAsync(listActivities);
            //        await _clientFriendRepository.DeleteManyAsync(listFriend);
            //        await _commentedRepository.DeleteManyAsync(commented);
            //        await _historiesRepository.DeleteManyAsync(histories);
            //    }    
            //}
            //catch { }
            //finally
            //{
            await _clientRepository.DeleteManyAsync(ids);
            //}
        }
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _clientBeLongToProfileRepository.DeleteAsync(x => x.ClientId == id);
                await _clientRepository.DeleteAsync(id);
            }
            catch
            {
                throw new UserFriendlyException($"Tài khoản đã thao tác và phát sinh dữ liệu. Không thể xóa!");
            }
            
        }

        public async Task<ClientDto> GetClientAsync(Guid Id)
        {
            var client = await _clientRepository.GetAsync(Id);

            return ObjectMapper.Map<Client, ClientDto>(client);
        }

        //1 Tác vụ chờ chạy
        //2 Tác vụ đang xử lý
        //3 Hoạt động
        //4 Bị checkpoint
        public async Task<AccountFacebookDto> GetListAccountFacebook(int skip, int take, int? conditionNumber)
        {
            var clients = await _clientRepository.GetListAsync();

            var clientsMap = ObjectMapper.Map<List<Client>, List<AccountDto>>(clients);

            clientsMap = clientsMap
                .WhereIf(conditionNumber.HasValue && conditionNumber.Value == 3, x => x.Online)
                .WhereIf(conditionNumber.HasValue && conditionNumber.Value == 1, x => IsClientScheduleJob(x.Id))
                .WhereIf(conditionNumber.HasValue && conditionNumber.Value == 2, x => _clientUsingScriptsRepository.Any(c => c.ClientId == x.Id && c.IsActive
                    && _scriptRepository.Any(s => s.IsActive && s.Id == c.ScriptId)))
                .WhereIf(conditionNumber.HasValue && conditionNumber.Value == 4, x => x.HasCheckPoint)
                .Skip(skip).Take(take).ToList();

            clientsMap.ForEach(x =>
            {
                if (_clientUsingScriptsRepository.Any(c => c.ClientId == x.Id && c.IsActive && _scriptRepository.Any(s => s.Id == c.ScriptId && s.IsActive)))
                {
                    var currentTask = _clientUsingScriptsRepository
                    .Where(c => c.ClientId == x.Id && c.IsActive && _scriptRepository.Any(s => s.Id == c.ScriptId && s.IsActive))
                    .OrderByDescending(c => c.LastModificationTime == null
                    ? c.CreationTime
                    : c.LastModificationTime)
                    .FirstOrDefault();

                    var script = _scriptRepository.FirstOrDefault(script => script.Id == currentTask.ScriptId);

                    var scriptMap = ObjectMapper.Map<Script, ClientUsingScriptDto>(script);

                    scriptMap.TypeName = EnumAppService.GetNameEnum<Type.Type>(scriptMap.Type);

                    CurrentTaskDto currentTaskDto = new()
                    {
                        ScriptName = scriptMap.ScriptName,
                        TypeName = scriptMap.TypeName,
                        Value = scriptMap.Value

                    };

                    x.CurrentTask = currentTaskDto;
                }

                x.TotalTask = GetTotalTask(x.Id);
                x.TotalTaskFinish = GetTotalTaskFinish(x.Id);
                x.TotalTaskWait = GetTotalTaskWait(x.Id);

                x.LastActivity = _clientActivityAppService.GetLastActivity(x.UserName);
            });

            AccountFacebookDto accountFacebookDto = new()
            {
                AccountDtos = new PagedResultDto<AccountDto>(clients.Count, clientsMap),
                TaskSchedule = GetTasksSchedule(),
                AccountCheckpoint = _clientRepository.Count(x => x.HasCheckPoint),
                TaskProcessing = _clientUsingScriptsRepository.Count(x => x.IsActive && _clientRepository.Any(c => c.Id == x.ClientId)),
                AccountOnline = _clientRepository.Count(x => x.Online)
            };

            return accountFacebookDto;
        }

        public int GetTotalTask(Guid Id)
        {
            return _hangfireJobRepository.Count(x => x.ClientId == Id);
        }

        public int GetTotalTaskFinish(Guid Id)
        {
            return _hangfireJobRepository.Count(x => x.ClientId == Id && x.IsFinish);
        }

        public int GetTotalTaskWait(Guid Id)
        {
            return _hangfireJobRepository.Count(x => x.ClientId == Id && !x.IsFinish);
        }

        public static bool IsClientScheduleJob(Guid clientId)
        {
            var jobStorage = JobStorage.Current.GetMonitoringApi();

            var isClientScheduleJob = jobStorage.ScheduledJobs(0, (int)jobStorage.ScheduledCount()).Any(x =>
            x.Value.Job.Type.Name == "IClientUsingScriptAppService"
            && JsonConvert.DeserializeObject<CreateClientUsingScriptDto>(x.Value.Job.Args.ToString()).ClientId == clientId);

            return isClientScheduleJob;
        }

        public static int GetTasksSchedule()
        {
            var jobStorage = JobStorage.Current.GetMonitoringApi();

            var tasksScheduleJob = jobStorage.ScheduledJobs(0, (int)jobStorage.ScheduledCount()).Count;

            return tasksScheduleJob;
        }

        public async Task<bool> CheckClientOnline(Guid clientId)
        {
            var client = await _clientRepository.GetAsync(clientId);

            return client.Online;
        }

        public async Task UpdateOnlineExceptionCloseChrome(UpdateOnlineDto updateOnlineDto)
        {
            var client = await _clientRepository.GetAsync(updateOnlineDto.ClientId);

            client.Online = updateOnlineDto.isOnline;

            await _clientRepository.UpdateAsync(client);
        }

    }
}

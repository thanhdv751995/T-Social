using Extention.Management.Clients;
using Extention.Management.Hub;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.ClientActivities
{
    public class ClientActivityAppService : ManagementAppService, IClientActivityAppService
    {
        private readonly IClientActivityRepository _clientActivitiesRepository;
        private readonly ClientActivityManager _clientFriendManager;
        private readonly IClientRepository _clientRepository;
        public readonly IHubContext<HubSignalR> _hub;
        public ClientActivityAppService(
          IClientActivityRepository clientActivitiesRepository,
          ClientActivityManager clientFriendManager,
          IClientRepository clientRepository,
          IHubContext<HubSignalR> hub)
        {
            _clientActivitiesRepository = clientActivitiesRepository;
            _clientFriendManager = clientFriendManager;
            _clientRepository = clientRepository;
            _hub = hub;
        }
        public async Task<ClientActivityDto> CreateAsync(CreateClientActivityDto input)
        {
            var clientActivity = _clientFriendManager.CreateAsync(input.UserName, input.Content, input.URL,input.ScriptName);

            await _clientActivitiesRepository.InsertAsync(clientActivity);

            var clientActivityMap = ObjectMapper.Map<ClientActivity, ClientActivityDto>(clientActivity);

            clientActivityMap.NameFacebook = _clientRepository.FirstOrDefault(x => x.UserName == input.UserName).NameFacebook;

            await _hub.Clients.All.SendAsync("NewActivities", $"{clientActivityMap.NameFacebook} {clientActivityMap.Content} đường dẫn {clientActivityMap.URL} thành công!");

            return clientActivityMap;
        }
        public async Task DeleteAsync(Guid id)
        {
            await _clientActivitiesRepository.DeleteAsync(id);
        }
        public async Task<PagedResultDto<ClientActivityDto>> GetListActivityByUserId(string userId, int skip, int take)
        {
            var clientActivity = await _clientActivitiesRepository.GetListAsync(x => x.UserName == userId);
            var clientActivityMap = ObjectMapper.Map<List<ClientActivity>, List<ClientActivityDto>>(clientActivity);
            clientActivityMap = clientActivityMap.Skip(skip).Take(take).ToList();

            return new PagedResultDto<ClientActivityDto>(
                   clientActivity.Count,
                   clientActivityMap
            );
        }

        public LastActivityDto GetLastActivity(string username)
        {
            LastActivityDto result = new();

            if(_clientActivitiesRepository.Any( x=> x.UserName == username))
            {
                var lastActivity = _clientActivitiesRepository.OrderByDescending(x => x.CreationTime).FirstOrDefault(x => x.UserName == username);
                result.ScriptName = lastActivity.ScriptName;
                result.URL = lastActivity.URL;
                result.TimeActivity = lastActivity.CreationTime;
            }
            return result;
        }
        public async Task<PagedResultDto<ClientActivityDto>> GetListHistoryActivity(DateTime? startDate, DateTime? endDate, string seedingName)
        {
            if (seedingName.IsNullOrWhiteSpace() || seedingName == "null")
            {
                seedingName = "";
            }
            DateTime now = DateTime.Now;
            DateTime localDate = new(now.Year, now.Month, now.Day, 0, 0, 0, 0);
            List<ClientActivity> clientActivities = new();
            if (startDate.HasValue && endDate.HasValue)
            {
                clientActivities = await _clientActivitiesRepository.GetListAsync(x => x.CreationTime >= startDate && x.CreationTime <= endDate && x.ScriptName.Contains(seedingName) );
            }    
            else
            {
                clientActivities = await _clientActivitiesRepository.GetListAsync(x => x.CreationTime > localDate && x.ScriptName.Contains(seedingName));
            }    
            var clientActivityMap = ObjectMapper.Map<List<ClientActivity>, List<ClientActivityDto>>(clientActivities);
            clientActivityMap.ForEach(x => x.NameFacebook = _clientRepository.FirstOrDefault(y => y.UserName == x.UserName).NameFacebook);
            clientActivityMap = clientActivityMap.OrderByDescending(x=>x.CreationTime).ToList();
            return new PagedResultDto<ClientActivityDto>(
                   clientActivities.Count,
                   clientActivityMap
            );
        }
    }
}

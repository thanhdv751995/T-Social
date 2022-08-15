using Extention.Management.ClientFriends;
using Extention.Management.Clients;
using Extention.Management.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Histories
{
    public class HistoryAppService : ManagementAppService, IHistoryAppService
    {
        private readonly IHistoryRepository _historiesRepository;
        private readonly HistoryManager _historyManager;
        private readonly IClientRepository _clientRepository;
        private readonly IScriptRepository _scriptRepository;
        public HistoryAppService(IHistoryRepository historiesRepository,
            HistoryManager historyManager,
            IClientRepository clientRepository,
            IScriptRepository scriptRepository)
        {
            _historiesRepository = historiesRepository;
            _historyManager = historyManager;
            _clientRepository = clientRepository;
            _scriptRepository = scriptRepository;
        }

        public async Task CreateAsync(CreateDto createDto)
        {
            var client = await _clientRepository.GetAsync(x => x.Id == createDto.ClientId);
            var history = _historyManager.CreateAsync(client.Id, createDto.ScriptId, null, null);

            await _historiesRepository.InsertAsync(history);
        }

        public async Task UpdateTimeStart(UpdateDateTimeDto updateDateTimeDto)
        {
            var history = await _historiesRepository.GetAsync(updateDateTimeDto.Id);

            history.TimeStart = DateTime.Now;

            await _historiesRepository.UpdateAsync(history);
        }

        public async Task UpdateTimeEnd(UpdateDateTimeDto updateDateTimeDto)
        {
            var history = await _historiesRepository.GetAsync(updateDateTimeDto.Id);

            history.TimeEnd = DateTime.Now;

            await _historiesRepository.UpdateAsync(history);
        }

        public List<HistoryDto> GetListAsync(int skip, int take, string clientId)
        {
            if (clientId.IsNullOrWhiteSpace() || clientId == "null")
            {
                clientId = "";
            }

            var histories = _historiesRepository.Where(x => x.ClientId.ToString().Contains(clientId)).Skip(skip).Take(take).ToList();

            var historiesMap = ObjectMapper.Map<List<History>, List<HistoryDto>>(histories);

            historiesMap.ForEach(x => {
                var client = _clientRepository.FirstOrDefault(c => c.Id == x.ClientId);

                x.UserName = client.UserName;
                x.ProxyIp = client.ProxyIp;
                //x.ScriptName = _scriptRepository.FirstOrDefault(s => s.Id == x.ScriptId).ScriptName;
            });

            return historiesMap;
        }

        public async Task DeleteManyAsync(Guid scriptId)
        {
            var histories = await _historiesRepository.GetListAsync(x => x.ScriptId == scriptId);

            await _historiesRepository.DeleteManyAsync(histories);
        }

    }
}

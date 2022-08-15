using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.ClientInfomations
{
    public class ClientInfomationAppService : ManagementAppService, IClientInfomationAppService
    {
        private readonly IClientInfomationRepository _clientInfomationRepository;
        private readonly ClientInfomationManager _clientInfomationManager;
        public ClientInfomationAppService(
          IClientInfomationRepository clientInfomationRepository,
          ClientInfomationManager clientInfomationManagerr)
        {
            _clientInfomationRepository = clientInfomationRepository;

            _clientInfomationManager = clientInfomationManagerr;
        }
        public async Task<ClientInfomationDto> CreateAsync(CreateUpdateClientInfomationDto input)
        {
            var clientInfo = _clientInfomationManager.CreateAsync(input.IdUser, input.ClientId, input.NameUser, input.DayOfBirth);

            await _clientInfomationRepository.InsertAsync(clientInfo);
            return ObjectMapper.Map<ClientInfomation, ClientInfomationDto>(clientInfo);
        }
        public async Task CreateUpdateByList(List<CreateUpdateClientInfomationDto> input)
        {
            if (input.Any())
            {
                foreach (var infoDto in input)
                {
                    if (_clientInfomationRepository.Any(x => x.IdUser == infoDto.IdUser))
                    {
                        var infoClient = await _clientInfomationRepository.GetAsync(x => x.IdUser == infoDto.IdUser);

                        infoClient.ClientId = infoDto.ClientId;
                        infoClient.DayOfBirth = infoDto.DayOfBirth;
                        infoClient.NameUser = infoDto.NameUser;

                        await _clientInfomationRepository.UpdateAsync(infoClient);
                    }
                    else
                    {
                        await CreateAsync(infoDto);
                    }
                }
            }
            //var listInfo = await _clientInfomationRepository.GetListAsync();
            //var createUpdateByListDto = ObjectMapper.Map<List<CreateUpdateClientInfomationDto>, List<ClientInfomation>>(input);
            //var test = listInfo.Intersect(createUpdateByListDto);
            //var test2 = createUpdateByListDto.Except(listInfo);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _clientInfomationRepository.DeleteAsync(id);
        }
        public async Task<ClientInfomationDto> GetAsync(Guid id)
        {
            var client = await _clientInfomationRepository.GetAsync(id);
            return ObjectMapper.Map<ClientInfomation, ClientInfomationDto>(client);
        }
        public async Task<PagedResultDto<ClientInfomationDto>> GetListInfoUser(int skip, int take)
        {
            var clientInfo = await _clientInfomationRepository.GetListAsync();
            var clientInfoMap = ObjectMapper.Map<List<ClientInfomation>, List<ClientInfomationDto>>(clientInfo);
            clientInfoMap = clientInfoMap.Skip(skip).Take(take).ToList();

            return new PagedResultDto<ClientInfomationDto>(
                   clientInfo.Count,
                   clientInfoMap
            );
        }
    }
}

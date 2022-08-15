using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.ClientFriends
{
    public class ClientFriendAppService : ManagementAppService, IClientFriendAppService
    {
        private readonly IClientFriendRepository _clientFriendRepository;
        private readonly ClientFriendManager _clientFriendManager;
        public ClientFriendAppService(
          IClientFriendRepository clientFriendRepository,
          ClientFriendManager clientFriendManager)
        {
            _clientFriendRepository = clientFriendRepository;
            _clientFriendManager = clientFriendManager;
        }
        public async Task<ClientFriendDto> CreateAsync(CreateClientFriendDto input)
        {
            bool isDuplicate = _clientFriendRepository.Any(x => x.IdUser == input.IdUser && x.UserName == input.UserName);
            if (!isDuplicate)
            {
                var clientFriend = _clientFriendManager.CreateAsync(input.IdUser, input.UserName, input.FriendName, input.AvatarUrl);
                await _clientFriendRepository.InsertAsync(clientFriend);
                return ObjectMapper.Map<ClientFriend, ClientFriendDto>(clientFriend);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<CreateClientFriendDto>> GetListFriend()
        {
            var query = await _clientFriendRepository.GetListAsync();
            var queryListFriendMap = ObjectMapper.Map<List<ClientFriend>, List<CreateClientFriendDto>>(query);
            return queryListFriendMap;
        }
        public async Task CreateUpdateByListAsync(List<CreateUpdateByListDto> input)
        {
            foreach (var ListInfoDto in input)
            {
                if (_clientFriendRepository.Any(x => x.IdUser == ListInfoDto.IdUser))
                {
                    var listFriend = await _clientFriendRepository.GetListAsync(x => x.IdUser == ListInfoDto.IdUser);
                    if (ListInfoDto.CreateClientFriendDtos.Any())
                    {
                        var exceptCLFCreate = ListInfoDto.CreateClientFriendDtos.Select(x => x.UserName).Except(listFriend.Select(x => x.UserName));

                        var createUpdateByListDto = ObjectMapper.Map<List<CreateClientFriendDto>, List<ClientFriend>>(ListInfoDto.CreateClientFriendDtos);
                        await _clientFriendRepository.InsertManyAsync(createUpdateByListDto.Where(x => exceptCLFCreate.Any(e => e == x.UserName)));
                    }
                    else
                    {
                        await _clientFriendRepository.DeleteManyAsync(listFriend);
                    }
                }
                else
                {
                    var createUpdateByListDto = ObjectMapper.Map<List<CreateClientFriendDto>, List<ClientFriend>>(ListInfoDto.CreateClientFriendDtos);
                    await _clientFriendRepository.InsertManyAsync(createUpdateByListDto);
                }
            }

            //var listFriend = await GetListFriend();
            //var FriendNoAccept = listFriend.Except(input);
            //if (FriendNoAccept != null)
            //{

            //}
        }
        public async Task DeleteAsync(Guid id)
        {
            await _clientFriendRepository.DeleteAsync(id);
        }
        public async Task<PagedResultDto<ClientFriendDto>> GetListFriendByUserId(Guid userId, int skip, int take)
        {
            var clientFriend = await _clientFriendRepository.GetListAsync(x => x.IdUser == userId);
            var clientFriendMap = ObjectMapper.Map<List<ClientFriend>, List<ClientFriendDto>>(clientFriend);
            clientFriendMap = clientFriendMap.Skip(skip).Take(take).ToList();
            return new PagedResultDto<ClientFriendDto>(
                   clientFriend.Count,
                   clientFriendMap
            );
        }
        public PagedResultDto<FriendDto> ListFriend(Guid? idUser, int skip, int take)
        {
            var query = _clientFriendRepository.WhereIf(idUser.HasValue, x => x.IdUser == idUser);
            var result = query.Skip(skip).Take(take).ToList();
            var friendsMap = ObjectMapper.Map<List<ClientFriend>, List<FriendDto>>(result);

            return new PagedResultDto<FriendDto> { 
                TotalCount = query.Count(),
                Items = friendsMap
            }
                ;
        }
    }
}

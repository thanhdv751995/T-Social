using Extention.Management.Clients;
using Extention.Management.Enums;
using Extention.Management.GroupJoins;
using Extention.Management.GroupTypes;
using Extention.Management.Randoms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.GroupJoin
{
    public class GroupJoinAppService : ManagementAppService, IGroupJoinAppService
    {
        private readonly IGroupJoinRepository _groupJoinRepository;
        private readonly GroupJoinManager _groupJoinManager;
        private readonly IClientRepository _clientRepository;
        private readonly RandomAppService _randomAppService;
        private readonly IGroupTypeRepository _groupTypeRepository;
        public GroupJoinAppService(IGroupJoinRepository groupJoinRepository,
            GroupJoinManager groupJoinManager,
            IClientRepository clientRepository,
            RandomAppService randomAppService,
            IGroupTypeRepository groupTypeRepository)
        {
            _groupJoinRepository = groupJoinRepository;
            _groupJoinManager = groupJoinManager;
            _clientRepository = clientRepository;
            _randomAppService = randomAppService;
            _groupTypeRepository = groupTypeRepository;
        }

        public async Task<PagedResultDto<GroupJoinDto>> GetListAsync(string userName, int take , int skip)
        {

            if (userName.IsNullOrWhiteSpace() || userName == "null")
            {
                userName = "";
            }

            var gJs = await _groupJoinRepository.GetListAsync(x => x.Username.Contains(userName));
            var value = gJs.Skip(skip).Take(take).ToList();
            var gJsMap = ObjectMapper.Map<List<GroupJoins.GroupJoin>, List<GroupJoinDto>>(value);
            return new PagedResultDto<GroupJoinDto>
            {
                Items = gJsMap,
                TotalCount = gJs.Count
            };
        }

        public async Task CreateAsync(CreateGJDto createGJDto)
        {
            GroupType groupType = null;

            var listGroupType = await _groupTypeRepository.GetListAsync();

            foreach(var item in listGroupType)
            {
                if (item.KeywordsRelative.Contains(","))
                {
                    var listSplitKeyword = item.KeywordsRelative.Split(",");

                    if (listSplitKeyword.Any(k => createGJDto.GroupName.Contains(k)))
                    {
                        groupType = item;
                    }
                }
                else
                {
                    if (createGJDto.GroupName.Contains(item.KeywordsRelative))
                    {
                        groupType = item;
                    }
                }
            }

            if(groupType != null)
            {
                var gJ = _groupJoinManager.CreateAsync(createGJDto.Username, createGJDto.GroupName, createGJDto.AvatarGroup, createGJDto.GroupURL, createGJDto.Content, groupType.Id);
                await _groupJoinRepository.InsertAsync(gJ);
            }
        }

        public async Task<List<string>> GetListGrounpRandomAsync(string userName, Guid groupTypeId, int? take)
        {
            var client = await _clientRepository.GetAsync(x => x.UserName == userName);
            List<GroupJoins.GroupJoin> gJs = await _groupJoinRepository.GetListAsync(x => x.GroupTypeId == groupTypeId);
            var listGroupName = gJs.Select(x => x.GroupName).ToList();

            if (take.HasValue)
            {
                List<string> result = new();

                for (var i = 0; i < take; i++)
                {
                    int rand = _randomAppService.RandomInt(0, listGroupName.Count);

                    while (result.Any(x => x == gJs[rand].GroupName))
                    {
                        rand = _randomAppService.RandomInt(0, listGroupName.Count);
                    }

                    result.Add(gJs[rand].GroupName);
                }

                listGroupName = result;
            }

            return listGroupName;
        }
    }
}

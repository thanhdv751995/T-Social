using Extention.Management.ClientFriends;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Controllers.ClientFriends
{
    [Microsoft.AspNetCore.Mvc.Route("api/friend")]
    public class CliendFriendController : ManagementController
    {
        private readonly ClientFriendAppService _clientFriendAppService;
        public CliendFriendController(ClientFriendAppService clientFriendAppService)
        {
            _clientFriendAppService = clientFriendAppService;
        }
        [HttpPost("Create")]
        public async Task<ClientFriendDto> CreateAsync([FromBody] CreateClientFriendDto input)
        {
            var client = await _clientFriendAppService.CreateAsync(input);

            return client;
        }
        [HttpDelete("Delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _clientFriendAppService.DeleteAsync(id);
        }
        [HttpGet("Get-List-Friend-By-UserId")]
        public async Task<PagedResultDto<ClientFriendDto>> GetListFriendByUserId(Guid userId, int skip, int take)
        {
            var friend = await _clientFriendAppService.GetListFriendByUserId(userId, skip, take);
            return friend;
        }
        [HttpPost("Create-By-List")]
        public async Task CreateByListAsync([FromBody]List<CreateUpdateByListDto> input)
        {
             await _clientFriendAppService.CreateUpdateByListAsync(input);
        }
        //[HttpGet("Get-List-Friend")]
        //public async Task<List<CreateClientFriendDto>> GetListFriend()
        //{
        //   return await _clientFriendAppService.GetListFriend();
        //}
        [HttpGet("Get-List-Friend")]
        public PagedResultDto<FriendDto> ListFriend(Guid? idUser, int skip, int take)
        {
            return  _clientFriendAppService.ListFriend(idUser, skip, take);
        }
    }
}

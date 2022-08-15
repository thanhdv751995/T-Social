using ExtensionsManagement.ClientFacebookDtos;
using ExtensionsManagement.ClientFacebooks;
using Extention.Management.Client;
using Extention.Management.Clients;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Controllers.Clients
{
    [Microsoft.AspNetCore.Mvc.Route("api/client")]
    public class ClientController : ManagementController
    {
        private readonly ClientAppService _clientService;
        public ClientController(ClientAppService clientService)
        {
            _clientService = clientService;
        }
        [HttpPost("Create")]
        public async Task<ClientDto> CreateAsync([FromBody] CreateUpdateClientDto input)
        {
            var client = await _clientService.CreateAsync(input);

            return client;
        }
        [HttpPost("Create-with-excel")]
        public async Task CreateWithExcel([FromBody]List<AddAcountWithExcelDto> input)
        {
             await _clientService.CreateWithExcel(input);

        }
        [HttpGet("Get-By-Id")]
        public async Task<ClientDto> GetAsync(Guid id)
        {
            var client = await _clientService.GetAsync(id);

            return client;
        }
        [HttpGet("Get-List")]
        public async Task<PagedResultDto<ClientDto>> GetListAsync(GetClientListDto input)
        {
            var client = await _clientService.GetListAsync(input);

            return client;
        }
        [HttpPut]
        public async Task UpdateAsync(Guid id, [FromBody] CreateUpdateClientDto input)
        {
            await _clientService.UpdateAsync(id, input);
        }
        [HttpGet("Get-List-Account-Active")]
        public async Task<PagedResultDto<ClientDto>> GetListClientActive(string nameClient , string profileName, string proxyIp, int skip, int take)
        {
            return await _clientService.GetListClientActive(nameClient , profileName, proxyIp,  skip,  take);
        }

        [HttpPut("Update-Active")]
        public async Task<bool> UpdateActive([FromBody] UpdateActiveDto updateActiveDto)
        {
            var rs = await _clientService.UpdateActive(updateActiveDto);
            return rs;
        }
        [HttpDelete("Delete-Many")]
        public async Task DeleteAsync([FromBody] deleteAccoutDto id)
        {
            await _clientService.DeleteManyAsync(id.id);
        }
        [HttpDelete("Delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _clientService.DeleteAsync(id);
        }
        [HttpGet("Get-Client")]
        public async Task<ClientDto> GetClientAsync(Guid Id)
        {
            return await _clientService.GetClientAsync(Id);
        }
        [HttpGet("Get-List-Client")]
        public async Task<List<ClientDto>> GetListClientAsync()
        {
            return await _clientService.GetListClientAsync();
        }
        [HttpGet("Get-List-Client-By-IDScript")]
        public async Task<List<ClientDto>> GetListClientFromIdScript(Guid scriptId)
        {
            return await _clientService.GetListClientFromIdScript(scriptId);
        }
        [HttpPut("Update-Online")]
        public async Task UpdateOnline([FromBody] UpdateOnlineDto input)
        {
            await _clientService.UpdateOnline(input);
        }
        [HttpPut("Update-Client-Online")]
        public async Task UpdateClientOnline([FromBody] UpdateClientOnlineDto updateClientOnlineDto)
        {
            await _clientService.UpdateClientOnline(updateClientOnlineDto);
        }
        [HttpPut("Update-Client-Online-By-Chrome-Profile")]
        public async Task UpdateClientOnlineByChromeProfile([FromBody] UpdateClientOnlineByChromeProfileDto updateClientOnlineByChromeProfileDto)
        {
            await _clientService.UpdateClientOnlineByChromeProfile(updateClientOnlineByChromeProfileDto);
        }
        /// <summary>
        ///ConditionNumber:
        /// 1 Tài khoản đang chờ chạy
        ///2 Đang hoạt động
        ///3 Không hoạt động
        ///4 Bị checkpoint
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="conditionNumber">
        /// </param>
        /// <returns></returns>
        [HttpGet("Get-List-Account-Facebook")]
        public async Task<AccountFacebookDto> GetListAccountFacebook(int skip, int take, int? conditionNumber)
        {
            return await _clientService.GetListAccountFacebook(skip, take, conditionNumber);
        }
        [HttpGet("check-client-online")]
        public async Task<bool> CheckClientOnline(Guid clientId)
        {
            var result = await _clientService.CheckClientOnline(clientId);

            return result;
        }
        [HttpPut("update-online-exception-close-chrome")]
        public async Task UpdateOnlineExceptionCloseChrome([FromBody]UpdateOnlineDto updateOnlineDto)
        {
            await _clientService.UpdateOnlineExceptionCloseChrome(updateOnlineDto);
        }
    }
}

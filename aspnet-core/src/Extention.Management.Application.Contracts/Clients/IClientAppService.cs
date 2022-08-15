using ExtensionsManagement.ClientFacebookDtos;
using Extention.Management.Client;
using Extention.Management.Clients;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ExtensionsManagement.ClientFacebooks
{
    public interface IClientAppService : IApplicationService
    {
        Task<PagedResultDto<ClientDto>> GetListClientActive(string nameClient, string profileName, string proxyIp, int skip, int take);
        Task<bool> UpdateActive(UpdateActiveDto updateActiveDto);
        Task<ClientDto> GetClientAsync(Guid Id);
        Task<List<ClientDto>> GetListClientAsync();
        Task UpdateClientOnline(UpdateClientOnlineDto updateClientOnlineDto);
        Task UpdateClientOnlineByChromeProfile(UpdateClientOnlineByChromeProfileDto updateClientOnlineByChromeProfileDto);
        Task<AccountFacebookDto> GetListAccountFacebook(int skip, int take, int? conditionNumber);
        Task<List<ClientDto>> GetListClientFromIdScript(Guid scriptId);
        Task<bool> CheckClientOnline(Guid clientId);
        Task UpdateOnlineExceptionCloseChrome(UpdateOnlineDto updateOnlineDto);
    }
}

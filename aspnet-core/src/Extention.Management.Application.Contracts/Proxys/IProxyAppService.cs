using ExtensionsManagement.ProxyIpDtos;
using Extention.Management.Proxys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ExtensionsManagement.ProxyIps
{
    public interface IProxyAppService : IApplicationService
    {
        Task PingHostAsync(HostsDto hostsDto);
        Task<ProxyDto> GetAsync(string IP);
        object GetProxyAndAccountConfigAsync();
        string GetProxyByUID(string UID);
        Task UpdateActiveAsync(UpdateActiveDto updateActiveDto);
    }
}

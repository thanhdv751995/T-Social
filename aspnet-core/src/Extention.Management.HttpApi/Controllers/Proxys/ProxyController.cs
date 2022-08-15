using ExtensionsManagement.ProxyIpDtos;
using ExtensionsManagement.ProxyIps;
using Extention.Management.Proxy;
using Extention.Management.Proxys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Controllers.Proxys
{
    [Microsoft.AspNetCore.Mvc.Route("api/proxy")]
    public class ProxyController : ManagementController
    {

        private readonly ProxyAppService _proxyAppService;
        public ProxyController(ProxyAppService proxyAppService)
        {
            _proxyAppService = proxyAppService;
        }
        [HttpPost("Create")]
        public async Task<ProxyDto> CreateAsync([FromBody] CreateUpdateProxyDto input)
        {
            var client = await _proxyAppService.CreateAsync(input);

            return client;
        }
        [HttpGet("Get-By-IP")]
        public async Task<ProxyDto> GetAsync(string IP)
        {
            var client = await _proxyAppService.GetAsync(IP);

            return client;
        }
        [HttpGet("Get-List")]
        public async Task<PagedResultDto<ProxyDto>> GetListAsync(GetProxyListDto input)
        {
            var client = await _proxyAppService.GetListAsync(input);

            return client;
        }
        /// <summary>
        /// Ping Host by IP Address: Ex: 127.0.0.1
        /// </summary>
        /// <returns></returns>
        [HttpPost("Ping-Hosts")]
        public async Task PingHostAsync([FromBody]HostsDto hostsDto)
        {
            await _proxyAppService.PingHostAsync(hostsDto);
        }
        /// <summary>
        /// Get FB Account and Proxy IP not Active for extension to config
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-Account-Proxy-Config")]
        public object GetProxyAndAccountConfigAsync()
        {
            return _proxyAppService.GetProxyAndAccountConfigAsync();
        }
        [HttpPut("Update-Active")]
        public async Task UpdateAsync(string id)
        {
            await _proxyAppService.UpdateActive(id);
        }
        [HttpGet("Get-Proxy-By-UID")]
        public string GetProxyByUID(string UID)
        {
            return _proxyAppService.GetProxyByUID(UID);
        }
        [HttpPut("Update-Active-By-Proxy-Ip")]
        public async Task UpdateActiveAsync([FromBody]UpdateActiveDto updateActiveDto)
        {
            await _proxyAppService.UpdateActiveAsync(updateActiveDto);
        }
    }
}

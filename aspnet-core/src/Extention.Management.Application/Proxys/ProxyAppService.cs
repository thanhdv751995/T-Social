using ExtensionsManagement.ProxyIpDtos;
using ExtensionsManagement.ProxyIps;
using Extention.Management.ChromeProfile;
using Extention.Management.Clients;
using Extention.Management.Hub;
using Extention.Management.Proxy;
using Extention.Management.Randoms;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Extention.Management.Proxys
{
    public class ProxyAppService : ManagementAppService, IProxyAppService
    {
        private readonly IProxyRepository _proxyRepository;
        private readonly ProxyManager _proxyManager;
        public IHubContext<HubSignalR> _hub;
        private readonly IClientRepository _clientRepository;
        private readonly CProfileAppService _cProfileAppService;
        private readonly IConfiguration _configuration;
        public ProxyAppService(IProxyRepository proxyRepository,
         ProxyManager proxyManager,
         IHubContext<HubSignalR> hub,
         IClientRepository clientRepository,
         CProfileAppService cProfileAppService,
         IConfiguration configuration)
        {
            _proxyRepository = proxyRepository;
            _proxyManager = proxyManager;
            _hub = hub;
            _clientRepository = clientRepository;
            _cProfileAppService = cProfileAppService;
            _configuration = configuration;
        }
        public async Task<ProxyDto> GetAsync(string IP)
        {
            var proxy = await _proxyRepository.GetAsync(x => x.ProxyIp == IP);
            return ObjectMapper.Map<Proxy, ProxyDto>(proxy);
        }
        public async Task<ProxyDto> CreateAsync(CreateUpdateProxyDto input)
        {
            if (_proxyRepository.Any(x => x.ProxyIp == input.ProxyIp))
            {
                return null;
            }

            var proxy = _proxyManager.CreateAsync(input.ProxyIp, false);

            await _proxyRepository.InsertAsync(proxy);

            return ObjectMapper.Map<Proxy, ProxyDto>(proxy);
        }
        public async Task<PagedResultDto<ProxyDto>> GetListAsync(GetProxyListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Proxy.CreationTime);
            }

            var totalCount = input.Filter == null
                ? await _proxyRepository.CountAsync()
                : await _proxyRepository.CountAsync(
                    client => client.ProxyIp.Contains(input.Filter));

            var clients = await _proxyRepository.GetListAsync(
                input.SkipCount,
                totalCount,
                input.Sorting,
                input.Filter
            );

            var clientsMap = ObjectMapper.Map<List<Proxy>, List<ProxyDto>>(clients);

            clientsMap.ForEach(x => x.AccountOnProxy = _clientRepository.Count(c => c.ProxyIp == x.ProxyIp));

            return new PagedResultDto<ProxyDto>(
                totalCount,
                clientsMap
            );
        }

        public async Task PingHostAsync(HostsDto hostsDto)
        {
            var ping = new Ping();

            try
            {
                foreach (string host in hostsDto.Hosts)
                {
                    try
                    {
                        PingReply reply = ping.Send(host);
                        bool isPingSuccess = reply.Status == IPStatus.Success;

                        await _hub.Clients.All.SendAsync("PingHost", host, isPingSuccess ? "success" : "fail");
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            catch (Exception e)
            {
                throw new UserFriendlyException("Ping Host Error ", e.Message);
            }
        }

        public async Task UpdateActive(string IP)
        {
            var proxyClient = await _proxyRepository.GetAsync(x => x.ProxyIp == IP);
            proxyClient.IsActive = !proxyClient.IsActive;

            await _proxyRepository.UpdateAsync(proxyClient);
        }

        public object GetProxyAndAccountConfigAsync()
        {
            _cProfileAppService.GetCurrentCProfile();

            var accountList = _clientRepository.ToList();

            var account = _clientRepository.FirstOrDefault(x => !x.IsActive);

            Proxy proxy = _proxyRepository.Any(x => !x.IsActive)
                    ? _proxyRepository.FirstOrDefault(x => !x.IsActive)
                    : _proxyRepository.FirstOrDefault(x =>
                    (_clientRepository.Count(client => client.ProxyIp == x.ProxyIp) < int.Parse(_configuration.GetSection("FacebookParameter")["LimitAccountOnProxy"])));

            if (account != null && proxy != null)
            {
                return new
                {
                    clientId = account.Id,
                    userName = account.UserName,
                    nameFacebook = account.NameFacebook,
                    password = account.Password,
                    cookie = account.Cookie,
                    f2FA = account.SecretKey,
                    proxyIp = proxy.ProxyIp,
                    isActive = proxy.IsActive,
                    idUser = account.Id,
                    accessToken = account.AccessToken
                };
            }
            else
            {
                return new { };
            }
        }

        public string GetProxyByUID(string UID)
        {
            var proxyIp = string.Empty;

            if (_clientRepository.Any(x => x.UserName == UID && !string.IsNullOrWhiteSpace(x.ProxyIp) && !string.IsNullOrWhiteSpace(x.ChromeProfile)))
            {
                proxyIp = _clientRepository.FirstOrDefault(x => x.UserName == UID).ProxyIp;
            }

            return proxyIp;
        }

        public async Task UpdateActiveAsync(UpdateActiveDto updateActiveDto)
        {
            var proxy = await _proxyRepository.GetAsync(x => x.ProxyIp == updateActiveDto.ProxyIp);

            proxy.IsActive = !proxy.IsActive;

            await _proxyRepository.UpdateAsync(proxy);
        }
    }
}

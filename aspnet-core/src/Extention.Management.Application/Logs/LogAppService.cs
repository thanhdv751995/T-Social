using Extention.Management.Hub;
using Extention.Management.Proxys;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Logs
{
    public class LogAppService : ManagementAppService, ILogsAppService
    {
        private readonly IProxyClientLogsRepository _proxyClientLogsRepository;
        private readonly ProxyClientsLogManager _proxyClientsLogManager;
        private readonly IProxyRepository _proxyRepository;
        public IHubContext<HubSignalR> _hub;
        public LogAppService(IProxyClientLogsRepository proxyClientLogsRepository,
            ProxyClientsLogManager proxyClientsLogManager,
            IProxyRepository proxyRepository,
            IHubContext<HubSignalR> hub)
        {
            _proxyClientLogsRepository = proxyClientLogsRepository;
            _proxyClientsLogManager = proxyClientsLogManager;
            _proxyRepository = proxyRepository;
            _hub = hub;
        }

        public async Task CreateLogAsync(CreateProxyClientLogDto proxyClientLogDto)
        {
            if (_proxyRepository.Any(x => x.ProxyIp == proxyClientLogDto.ProxyIP))
            {
                var proxyClientLog = _proxyClientsLogManager.CreateAsync(proxyClientLogDto.ProxyIP, proxyClientLogDto.Body, proxyClientLogDto.Method);

                var log = await _proxyClientLogsRepository.InsertAsync(proxyClientLog);
                log.CreationTime = DateTime.Now;
                //await _hub.Clients.All.SendAsync("CreatedLog", log);
            }
        }

        public async Task<List<ProxyCientLogDto>> GetListLogs(string IP, string date, int skip, int take)
        {
            if (date.IsNullOrWhiteSpace() || date == "null")
            {
                date = "";
            }

            var logs = await _proxyClientLogsRepository.GetListAsync(x => x.ProxyIp == IP && x.CreationTime.ToString().Contains(date));
            logs = logs.OrderByDescending(x => x.CreationTime).Skip(skip).Take(take).ToList();

            return ObjectMapper.Map<List<ProxyClientLog>, List<ProxyCientLogDto>>(logs);
        }
    }
}

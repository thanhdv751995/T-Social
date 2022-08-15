using Extention.Management.Clients;
using Extention.Management.Hub;
using Extention.Management.VirtualMachines;
using Microsoft.AspNetCore.SignalR;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;

namespace Extention.Management.Chrome
{
    public class ChromeAppService : ManagementAppService, IChromeAppService
    {
        private readonly IClientRepository _clientRepository;
        public readonly IHubContext<HubSignalR> _hub;
        private readonly VirtualMachineAppService _virtualMachineAppService;
        public ChromeAppService(IClientRepository clientRepository,
            IHubContext<HubSignalR> hub,
            VirtualMachineAppService virtualMachineAppService)
        {
            _clientRepository = clientRepository;
            _hub = hub;
            _virtualMachineAppService = virtualMachineAppService;
        }

        public async Task StartChromeAsync(string profile, string url)
        {
            if (!profile.IsNullOrWhiteSpace() && profile != "undefined")
            {
                var client = await _clientRepository.GetAsync(x => x.ChromeProfile == profile);

                if (!client.Online)
                {
                    await _hub.Clients.All.SendAsync("OpenChromeWindowService", profile, client.ComputerName, url);
                }
            }
        }

        public async Task NewChromeProfileAsync(string profile, string url)
        {
            if (!profile.IsNullOrWhiteSpace() && profile != "undefined")
            {
                var VM = _virtualMachineAppService.GetVM();
                await _hub.Clients.All.SendAsync("OpenChromeWindowService", profile, VM, url);
            }
        }
    }
}

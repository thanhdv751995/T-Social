using Extention.Management.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace Extention.Management.EventHandler.ProxyHandler
{
    public class CreateProxyHandler : ILocalEventHandler<EntityCreatedEventData<Proxys.Proxy>>,
          ITransientDependency
    {
        private readonly HubAppService _hubAppService;
        public CreateProxyHandler(HubAppService hubAppService)
        {
            _hubAppService = hubAppService;
        }
        public async Task HandleEventAsync(EntityCreatedEventData<Proxys.Proxy> eventData)
        {
            await _hubAppService.SendAsync("InstallProxyService");
        }
    }
}

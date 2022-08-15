using Extention.Management.AccountUsingScripts;
using Extention.Management.ProxyUsingScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace Extention.Management.EventHandler.ClientUsingScriptHandler
{
    public class UpdatingClientUsingScriptHandler : ILocalEventHandler<EntityUpdatingEventData<ClientUsingScript>>,
          ITransientDependency
    {
        private readonly ClientUsingScriptAppService _clientUsingScriptAppService;
        public UpdatingClientUsingScriptHandler(
            ClientUsingScriptAppService clientUsingScriptAppService)
        {
            _clientUsingScriptAppService = clientUsingScriptAppService;
        }

        public async Task HandleEventAsync(EntityUpdatingEventData<ClientUsingScript> eventData)
        {
            var entity = eventData.Entity;

            await _clientUsingScriptAppService.UpdatingScriptCloseChromeHandler(entity.ScriptId, entity.ClientId);
        }
    }
}


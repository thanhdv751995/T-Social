using Extention.Management.AccountUsingScripts;
using Extention.Management.KafkaProducer;
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
    public class CreateClientUsingScriptHandler : ILocalEventHandler<EntityCreatedEventData<ClientUsingScript>>,
          ITransientDependency
    {
        private readonly KafkaProducerAppService _kafkaProducerAppService;
        private readonly ClientUsingScriptAppService _clientUsingScriptAppService;
        public CreateClientUsingScriptHandler(KafkaProducerAppService kafkaProducerAppService,
            ClientUsingScriptAppService clientUsingScriptAppService)
        {
            _kafkaProducerAppService = kafkaProducerAppService;
            _clientUsingScriptAppService = clientUsingScriptAppService;
        }

        public async Task HandleEventAsync(EntityCreatedEventData<ClientUsingScript> eventData)
        {
            var entity = eventData.Entity;

            KafkaActiviteChartDto kafkaActiviteChartDto = new()
            {
                ClientId = entity.ClientId,
                ScriptId = entity.ScriptId
            };

            //await _clientUsingScriptAppService.InvokeOpenChrome(entity.ClientId);
            await _clientUsingScriptAppService.UpdatingScriptCloseChromeHandler(entity.ScriptId, entity.ClientId);
            await _kafkaProducerAppService.ProducerClientActivities(kafkaActiviteChartDto);
        }
    }
}

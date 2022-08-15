using Extention.Management.AccountUsingScripts;
using Extention.Management.Clients;
using Extention.Management.Commenteds;
using Extention.Management.KafkaProducer;
using Extention.Management.ProxyUsingScript;
using Extention.Management.Scripts;
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
    public class UpdateClientUsingScriptHandler : ILocalEventHandler<EntityUpdatedEventData<ClientUsingScript>>,
          ITransientDependency
    {
        private readonly KafkaProducerAppService _kafkaProducerAppService;
        private readonly CommentedAppService _commentedAppService;
        private readonly IScriptRepository _scriptRepository;
        private readonly ClientUsingScriptAppService _clientUsingScriptAppService;
        public UpdateClientUsingScriptHandler(KafkaProducerAppService kafkaProducerAppService,
            CommentedAppService commentedAppService,
            IScriptRepository scriptRepository,
            ClientUsingScriptAppService clientUsingScriptAppService)
        {
            _kafkaProducerAppService = kafkaProducerAppService;
            _commentedAppService = commentedAppService;
            _scriptRepository = scriptRepository;
            _clientUsingScriptAppService = clientUsingScriptAppService;
        }

        public async Task HandleEventAsync(EntityUpdatedEventData<ClientUsingScript> eventData)
        {
            var entity = eventData.Entity;

            KafkaActiviteChartDto kafkaActiviteChartDto = new()
            {
                ClientId = entity.ClientId,
                ScriptId = entity.ScriptId
            };

            var script = await _scriptRepository.GetAsync(entity.ScriptId);

            await _commentedAppService.CreateByHandlerUpdateAsync(script, entity.ClientId);
            await _kafkaProducerAppService.ProducerClientActivities(kafkaActiviteChartDto);        }
    }
}

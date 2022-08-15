using Extention.Management.Histories;
using Extention.Management.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace Extention.Management.EventHandler.ScriptHandler
{
    public class DeleteScriptHandler : ILocalEventHandler<EntityDeletedEventData<Script>>,
          ITransientDependency
    {
        private readonly HistoryAppService _historyAppService;
        public DeleteScriptHandler(HistoryAppService historyAppService)
        {
            _historyAppService = historyAppService;
        }
        public async Task HandleEventAsync(EntityDeletedEventData<Script> eventData)
        {
            await _historyAppService.DeleteManyAsync(eventData.Entity.Id);
        }
    }
}

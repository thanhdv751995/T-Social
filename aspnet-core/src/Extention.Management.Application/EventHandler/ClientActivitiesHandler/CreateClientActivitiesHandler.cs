using Extention.Management.ClientActivities;
using Extention.Management.Randoms;
using Extention.Management.Seedings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace Extention.Management.EventHandler.ClientActivitiesHandler
{
    public class CreateClientActivitiesHandler : ILocalEventHandler<EntityCreatedEventData<ClientActivity>>,
          ITransientDependency
    {
        private readonly SeedingAppService _seedingAppService;
        private readonly RandomAppService _randomAppService;
        public CreateClientActivitiesHandler(SeedingAppService seedingAppService,
            RandomAppService randomAppService)
        {
            _seedingAppService = seedingAppService;
            _randomAppService = randomAppService;
        }
        public async Task HandleEventAsync(EntityCreatedEventData<ClientActivity> eventData)
        {
            var entity = eventData.Entity;

            if (entity.Content.Contains(Type.Type.PostStatus.ToString()) || entity.Content.Contains(Type.Type.PostGroup.ToString()))
            {
                int numberRandom = entity.Content.Contains(Type.Type.PostStatus.ToString()) ? 4 : 3;
                int delayTime = 0;

                List<int> randomList = new();

                while (randomList.Count < numberRandom)
                {
                    var randomInt = _randomAppService.RandomInt(0, numberRandom);

                    while (randomList.Any(x => x == randomInt))
                    {
                        randomInt = _randomAppService.RandomInt(0, numberRandom);
                    }

                    delayTime += _randomAppService.RandomInt(2000, 7000);

                    switch (randomInt)
                    {
                        case 0:
                            await _seedingAppService.ReactPost(entity.ScriptName, entity.URL, entity.UserName, delayTime);
                            break;
                        case 1:
                            await _seedingAppService.CommentPost(entity.ScriptName, entity.URL, entity.UserName, delayTime);
                            break;
                        case 2:
                            await _seedingAppService.SharePost(entity.ScriptName, entity.URL, entity.UserName, delayTime);
                            break;
                        case 3:
                            await _seedingAppService.SharePostToGroup(entity.ScriptName, entity.URL, entity.UserName, delayTime);
                            break;
                    }

                    randomList.Add(randomInt);
                }
            }
        }
    }
}

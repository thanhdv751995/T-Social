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

namespace Extention.Management.EventHandler.SeedingHandler
{
    public class CreateSeedingHandler : ILocalEventHandler<EntityCreatedEventData<Seeding>>,
          ITransientDependency
    {
        private readonly SeedingAppService _seedingAppService;
        private readonly RandomAppService _randomAppService;
        public CreateSeedingHandler(SeedingAppService seedingAppService,
            RandomAppService randomAppService)
        {
            _seedingAppService = seedingAppService;
            _randomAppService = randomAppService;
        }
        public async Task HandleEventAsync(EntityCreatedEventData<Seeding> eventData)
        {
            var entity = eventData.Entity;

            if (!entity.URL.IsNullOrWhiteSpace())
            {
                int numberRandom = 4;
                int delayTime = 0;

                List<int> randomList = new();

                while (randomList.Count < numberRandom)
                {
                    var randomInt = _randomAppService.RandomInt(0, numberRandom);

                    while (randomList.Any(x => x == randomInt))
                    {
                        randomInt = _randomAppService.RandomInt(0, numberRandom);
                    }

                    delayTime += _randomAppService.RandomInt(1000, 5000);

                    switch (randomInt)
                    {
                        case 0:
                            await _seedingAppService.ReactPost(entity.Name, entity.URL, string.Empty, delayTime);
                            break;
                        case 1:
                            await _seedingAppService.CommentPost(entity.Name, entity.URL, string.Empty, delayTime);
                            break;
                        case 2:
                            await _seedingAppService.SharePost(entity.Name, entity.URL, string.Empty, delayTime);
                            break;
                        case 3:
                            await _seedingAppService.SharePostToGroup(entity.Name, entity.URL, string.Empty, delayTime);
                            break;
                    }

                    randomList.Add(randomInt);
                }
            }
        }
    }
}

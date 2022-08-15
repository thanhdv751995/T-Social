using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.ReactedSeedingUrls
{
    public class ReactedSeedingUrlAppService : ManagementAppService
    {
        private readonly IReactedSeedingUrlRepository _reactedSeedingUrlRepository;
        private readonly ReactedSeedingUrlManager _reactedSeedingUrlManager;
        public ReactedSeedingUrlAppService(IReactedSeedingUrlRepository reactedSeedingUrlRepository,
            ReactedSeedingUrlManager reactedSeedingUrlManager)
        {
            _reactedSeedingUrlRepository = reactedSeedingUrlRepository;
            _reactedSeedingUrlManager = reactedSeedingUrlManager;
        }

        public async Task CreateAsync(CreateReactedSeedingUrlDto createReactedSeedingUrlDto)
        {
            var reated = _reactedSeedingUrlManager.CreateAsync(createReactedSeedingUrlDto.ClientId, createReactedSeedingUrlDto.URL);

            await _reactedSeedingUrlRepository.InsertAsync(reated);
        }

        public async Task CreateManyAsync(List<CreateReactedSeedingUrlDto> createReactedSeedingUrlDtos)
        {
            List<ReactedSeedingUrl> reactedSeedingUrls = new();

            foreach (var createReactedSeedingUrlDto in createReactedSeedingUrlDtos)
            {
                var commented = _reactedSeedingUrlManager.CreateAsync(createReactedSeedingUrlDto.ClientId, createReactedSeedingUrlDto.URL);

                reactedSeedingUrls.Add(commented);
            }

            await _reactedSeedingUrlRepository.InsertManyAsync(reactedSeedingUrls);
        }
    }
}

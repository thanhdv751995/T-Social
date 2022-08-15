using Extention.Management.SeedingContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.SeedingContents
{
    public class SeedingContentAppService : ManagementAppService, ISeedingContentAppService
    {
        private readonly ISeedingContentRepository _seedingContentRepository;
        private readonly SeedingContentManager _seedingContentManager;
        public SeedingContentAppService(ISeedingContentRepository seedingContentRepository,
            SeedingContentManager seedingContentManager)
        {
            _seedingContentRepository = seedingContentRepository;
            _seedingContentManager = seedingContentManager;
        }

        public async Task CreateByListAsync(SeedingContentListDto seedingContentListDto)
        {
            List<SeedingContent> seedingContents = new();

            foreach (var seedingContent in seedingContentListDto.SeedingContentDtos)
            {
                var seedingContentCreate = _seedingContentManager.CreateAsync(seedingContentListDto.SeedingId, seedingContent.Content, seedingContent.ImageUrl);

                seedingContents.Add(seedingContentCreate);
            }

            await _seedingContentRepository.InsertManyAsync(seedingContents);
        }
    }
}

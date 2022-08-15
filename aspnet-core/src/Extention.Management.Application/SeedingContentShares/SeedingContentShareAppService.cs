using Extention.Management.SeedingContentComment;
using Extention.Management.SeedingContentShare;
using Extention.Management.SeedingContentShares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management
{
    public class SeedingContentShareAppService : ManagementAppService
    {
        private readonly ISeedingContentShareRepository _seedingContentShareRepository;
        private readonly SeedingContentShareManager _seedingContentShareManager;
        public SeedingContentShareAppService(ISeedingContentShareRepository seedingContentShareRepository,
            SeedingContentShareManager seedingContentShareManager)
        {
            _seedingContentShareRepository = seedingContentShareRepository;
            _seedingContentShareManager = seedingContentShareManager;
        }

        public async Task CreateByListAsync(SeedingContentShareListDto seedingContentListDto)
        {
            List<SeedingContentShares.SeedingContentShare> seedingContents = new();

            foreach (var seedingContent in seedingContentListDto.SeedingContentDtos)
            {
                var seedingContentCreate = _seedingContentShareManager.CreateAsync(seedingContentListDto.SeedingId, seedingContent.Content);

                seedingContents.Add(seedingContentCreate);
            }

            await _seedingContentShareRepository.InsertManyAsync(seedingContents);
        }
    }
}

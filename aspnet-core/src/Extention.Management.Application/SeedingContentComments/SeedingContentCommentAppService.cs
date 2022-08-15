using Extention.Management.SeedingContent;
using Extention.Management.SeedingContentComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.SeedingContentComments
{
    public class SeedingContentCommentAppService : ManagementAppService
    {
        private readonly ISeedingContentCommentRepository _seedingContentCommentRepository;
        private readonly SeedingContentCommentManager _seedingContentCommentManager;
        public SeedingContentCommentAppService(ISeedingContentCommentRepository seedingContentCommentRepository,
            SeedingContentCommentManager seedingContentCommentManager)
        {
            _seedingContentCommentManager = seedingContentCommentManager;
            _seedingContentCommentRepository = seedingContentCommentRepository;
        }

        public async Task CreateByListAsync(SeedingContentCommentListDto seedingContentListDto)
        {
            List<SeedingContentComment> seedingContents = new();

            foreach (var seedingContent in seedingContentListDto.SeedingContentDtos)
            {
                var seedingContentCreate = _seedingContentCommentManager.CreateAsync(seedingContentListDto.SeedingId, seedingContent.Content);

                seedingContents.Add(seedingContentCreate);
            }

            await _seedingContentCommentRepository.InsertManyAsync(seedingContents);
        }
    }
}

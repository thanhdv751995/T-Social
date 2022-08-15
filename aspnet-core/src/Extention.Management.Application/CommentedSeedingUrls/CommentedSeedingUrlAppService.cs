using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.CommentedSeedingUrls
{
    public class CommentedSeedingUrlAppService : ManagementAppService, ICommentedSeedingUrlAppService
    {
        private readonly ICommentedSeedingUrlRepository _commentedSeedingUrlRepository;
        private readonly CommentedSeedingUrlManager _commentedSeedingUrlManager;
        public CommentedSeedingUrlAppService(ICommentedSeedingUrlRepository commentedSeedingUrlRepository,
            CommentedSeedingUrlManager commentedSeedingUrlManager)
        {
            _commentedSeedingUrlManager = commentedSeedingUrlManager;
            _commentedSeedingUrlRepository = commentedSeedingUrlRepository;
        }

        public async Task CreateAsync(CreateCommentedSeedingUrlDto createCommentedSeedingUrlDto)
        {
            var commented = _commentedSeedingUrlManager.CreateAsync(createCommentedSeedingUrlDto.ClientId, createCommentedSeedingUrlDto.URL);

            await _commentedSeedingUrlRepository.InsertAsync(commented);
        }

        public async Task CreateManyAsync(List<CreateCommentedSeedingUrlDto> createCommentedSeedingUrlDtos)
        {
            List<CommentedSeedingUrl> commentedSeedingUrls = new();

            foreach (var commentedSeedingUrlDto in createCommentedSeedingUrlDtos)
            {
                var commented = _commentedSeedingUrlManager.CreateAsync(commentedSeedingUrlDto.ClientId, commentedSeedingUrlDto.URL);

                commentedSeedingUrls.Add(commented);
            }

            await _commentedSeedingUrlRepository.InsertManyAsync(commentedSeedingUrls);
        }
    }
}

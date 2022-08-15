using Extention.Management.Scripts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Uow;

namespace Extention.Management.Commenteds
{
    public class CommentedAppService : ManagementAppService, ICommentedAppService
    {
        private readonly ICommentedRepository _commentedReponsitory;
        private readonly CommentedManager _commentedManager;
        private readonly IConfiguration _configuration;
        public CommentedAppService(
            ICommentedRepository commentedReponsitory,
            CommentedManager commentedManager,
            IConfiguration configuration
            )
        {
            _commentedReponsitory = commentedReponsitory;
            _commentedManager = commentedManager;
            _configuration = configuration;
        }
        public async Task<CommentedDto> CreateAsync(CreateCommentedDto input)
        {
            var commented = _commentedManager.CreateAsync(input.ClientId, input.CommentContent, input.URL);

            await _commentedReponsitory.InsertAsync(commented);
            return ObjectMapper.Map<Commented, CommentedDto>(commented);
        }
        public async Task<CommentedDto> GetAsync(Guid clientId)
        {
            var commented = await _commentedReponsitory.GetAsync(x => x.ClientId == clientId);
            return ObjectMapper.Map<Commented, CommentedDto>(commented);
        }
        public async Task<List<CommentedDto>> GetListCommentByClient(Guid clientId)
        {
            var commented = await _commentedReponsitory.GetListAsync(x => x.ClientId == clientId);
            return ObjectMapper.Map<List<Commented>, List<CommentedDto>>(commented);
        }
        [UnitOfWork]
        public async Task CreateByHandlerUpdateAsync(Script script, Guid clientId)
        {
            if (script.Type == Type.Type.CommentPost)
            {
                var valueSplit = script.Value.Split(_configuration["FacebookParameter:UrlCharacter"]);

                CreateCommentedDto createCommentedDto = new()
                {
                    ClientId = clientId,
                    CommentContent = valueSplit[1],
                    URL = valueSplit[0]
                };

                await CreateAsync(createCommentedDto);
            }
        }
    }
}

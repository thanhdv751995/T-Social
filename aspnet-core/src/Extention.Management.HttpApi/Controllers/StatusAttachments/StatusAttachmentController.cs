using Extention.Management.StatusAttachments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Controllers.StatusAttachments
{
    [Microsoft.AspNetCore.Mvc.Route("api/statusAttachment")]
    public class StatusAttachmentController : ManagementController
    {
        private readonly StatusAttachmentAppService _statusAttachmentAppService;
        public StatusAttachmentController(StatusAttachmentAppService statusAttachmentAppService)
        {

            _statusAttachmentAppService = statusAttachmentAppService;
        }
        [HttpPost("Create")]
        public async Task<StatusAttachmentDto> CreateAsync([FromBody] CreateUpdateStatusAttachment input)
        {
            var att = await _statusAttachmentAppService.CreateAsync(input);
            return att;
        }
        [HttpPost("Create-by-list")]
        public async Task CreateByListAttachment([FromBody] CreateByListAttachmentDto input)
        {
            await _statusAttachmentAppService.CreateByListAttachment(input);
        }
        //[HttpGet("Get-List-Attachment")]
        //public async Task<PagedResultDto<GetListAttachmentDto>> GetListAsync()
        //{
        //    var listAtt = await _statusAttachmentAppService.GetListAsync();
        //   return listAtt;
        //}
        //[HttpGet("Get-List-Attachment-By-UserId")]
        //public async Task<GetAttachmentByUserDto> GetAttachmentByIdUser(Guid idUser)
        //{
        //    return await _statusAttachmentAppService.GetAttachmentByIdUser(idUser);
        //}
    }
}

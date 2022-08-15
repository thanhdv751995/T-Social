using Extention.Management.AttachmentsStore;
using Extention.Management.AttachmentStore;
using Extention.Management.EStatusType;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.AttachmentsStore
{
    [Microsoft.AspNetCore.Mvc.Route("api/attachments-store")]
    public class AttachmentsStoreController : ManagementController
    {
        private readonly AttachmentsStoreAppService _attachmentsStoreAppService;
        public AttachmentsStoreController(AttachmentsStoreAppService attachmentsStoreAppService)
        {
            _attachmentsStoreAppService = attachmentsStoreAppService;
        }

        [HttpPost("Create")]
        public async Task CreateAsync([FromBody]CreateAttachmentsStoreDto createAttachmentsStoreDto)
        {
            await _attachmentsStoreAppService.CreateAsync(createAttachmentsStoreDto);
        }
        [HttpGet("attachment-random")]
        public async Task<string> GetAttachmentStoreRandom(StatusType statusType)
        {
            return await _attachmentsStoreAppService.GetAttachmentStoreRandom(statusType);
        }
    }
}

using Extention.Management.EStatusType;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Extention.Management.AttachmentsStore
{
    public interface IAttachmentsStoreAppService : IApplicationService
    {
        Task CreateAsync(CreateAttachmentsStoreDto createAttachmentsStoreDto);
        Task<string> GetAttachmentStoreRandom(StatusType statusType);
    }
}

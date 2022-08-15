using Extention.Management.AttachmentsStore;
using Extention.Management.EStatusType;
using Extention.Management.Randoms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.AttachmentStore
{
    public class AttachmentsStoreAppService : ManagementAppService
    {
        private readonly IAttachmentsStoreRepository _attachmentsStoreRepository;
        private readonly AttachmentStoreManager _attachmentStoreManager;
        private readonly RandomAppService _randomAppService;
        public AttachmentsStoreAppService(IAttachmentsStoreRepository attachmentsStoreRepository,
            AttachmentStoreManager attachmentStoreManager,
            RandomAppService randomAppService)
        {
            _attachmentsStoreRepository = attachmentsStoreRepository;
            _attachmentStoreManager = attachmentStoreManager;
            _randomAppService = randomAppService;
        }

        public async Task CreateAsync(CreateAttachmentsStoreDto createAttachmentsStoreDto)
        {
            var aS = _attachmentStoreManager.CreateAsync(createAttachmentsStoreDto.StatusType, createAttachmentsStoreDto.Url);

            await _attachmentsStoreRepository.InsertAsync(aS);
        }

        public async Task<string> GetAttachmentStoreRandom(StatusType statusType)
        {
            var attachment = string.Empty;

            if (_attachmentsStoreRepository.Any(x => x.StatusType == statusType))
            {
                var aSs = await _attachmentsStoreRepository.GetListAsync(x => x.StatusType == statusType);

                int rand = _randomAppService.RandomInt(0, aSs.Count);

                attachment = aSs[rand].URL;
            }

            return attachment;
        }
    }
}

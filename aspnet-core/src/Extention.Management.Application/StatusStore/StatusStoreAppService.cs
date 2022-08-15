using Extention.Management.AttachmentStore;
using Extention.Management.Enums;
using Extention.Management.EStatusType;
using Extention.Management.Randoms;
using Extention.Management.StatussStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.StatusStore
{
    public class StatusStoreAppService : ManagementAppService
    {
        private readonly IStatusStoreRepository _statusStoreRepository;
        private readonly StatusStoreManager _statusStoreManager;
        private readonly AttachmentsStoreAppService _attachmentsStoreAppService;
        private readonly RandomAppService _randomAppService;
        public StatusStoreAppService(IStatusStoreRepository statusStoreRepository,
            StatusStoreManager statusStoreManager,
            AttachmentsStoreAppService attachmentsStoreAppService,
            RandomAppService randomAppService)
        {
            _statusStoreRepository = statusStoreRepository;
            _statusStoreManager = statusStoreManager;
            _attachmentsStoreAppService = attachmentsStoreAppService;
            _randomAppService = randomAppService;
        }

        public async Task CreateAsync(CreateStatusStoreDto createStatusStoreDto)
        {
            var sS = _statusStoreManager.CreateAsync(createStatusStoreDto.StatusType, createStatusStoreDto.Content);

            await _statusStoreRepository.InsertAsync(sS);
        }

        public async Task<StatusStoreDto> GetStatusRandomAsync(StatusType statusType)
        {
            StatusStoreDto statusStore = new()
            {
                Message = string.Empty,
                Link = string.Empty
            };

            if (_statusStoreRepository.Any(x => x.StatusType == statusType))
            {
                var sSs = await _statusStoreRepository.GetListAsync(x => x.StatusType == statusType);
                int rand = _randomAppService.RandomInt(0, sSs.Count);

                var attachment = await _attachmentsStoreAppService.GetAttachmentStoreRandom(statusType);

                statusStore = new()
                {
                    Message = sSs[rand].Content,
                    Link = attachment
                };
            }

            return statusStore;
        }
    }
}

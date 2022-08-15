using Extention.Management.Statuss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;

namespace Extention.Management.Statuss
{
    public class StatusAppService : ManagementAppService, IStatusAppService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly StatusManager _statusManager;
        public StatusAppService(
         IStatusRepository statusRepository,
         StatusManager statusManager
        )
        {
            _statusRepository = statusRepository;
            _statusManager = statusManager;
        }
        public async Task<StatusDto> GetAsync(Guid id)
        {
            var status = await _statusRepository.GetAsync(id);
            return ObjectMapper.Map<Status, StatusDto>(status);
        }
        public async Task<StatusDto> CreateAsync(CreateUpdateStatusDto input)
        {
            var status = _statusManager.CreateAsync(input.IdUser, input.Content);

            await _statusRepository.InsertAsync(status);
            return ObjectMapper.Map<Status, StatusDto>(status);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _statusRepository.DeleteAsync(id);
        }
    }
}

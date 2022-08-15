using Extention.Management.PingClientExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.PingClientExtensions
{
    public class PingClientExtensionAppService : ManagementAppService
    {
        private readonly IPingClientExtensionRepository _pingClientExtensionRepository;
        private readonly PingClientExtensionManager _pingClientExtensionManager;
        public PingClientExtensionAppService(IPingClientExtensionRepository pingClientExtensionRepository,
            PingClientExtensionManager pingClientExtensionManager)
        {
            _pingClientExtensionRepository = pingClientExtensionRepository;
            _pingClientExtensionManager = pingClientExtensionManager;
        }

        public async Task CreateUpdatePingClientExtension(CreateUpdatePingClientExtensionDto createUpdatePingClientExtensionDto)
        {
            if(!_pingClientExtensionRepository.Any(x => x.ClientId == createUpdatePingClientExtensionDto.ClientId))
            {
                var pce = _pingClientExtensionManager.CreateAsync(createUpdatePingClientExtensionDto.ClientId, true);

                await _pingClientExtensionRepository.InsertAsync(pce);
            }
            else
            {
                var pce = await _pingClientExtensionRepository.GetAsync(x => x.ClientId == createUpdatePingClientExtensionDto.ClientId);

                pce.IsPingSuccess = true;

                await _pingClientExtensionRepository.UpdateAsync(pce);
            }

        }
    }
}

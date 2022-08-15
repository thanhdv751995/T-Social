using Extention.Management.CChromeProfile;
using Extention.Management.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.ChromeProfile
{
    public class CProfileAppService : ManagementAppService, IChromeProfileAppService
    {
        private readonly IChromeProfileRepository _chromeProfileRepository;
        private readonly ChromeProfileManager _chromeProfileManager;
        private readonly IClientRepository _clientRepository;
        public CProfileAppService(IChromeProfileRepository chromeProfileRepository,
            ChromeProfileManager chromeProfileManager,
            IClientRepository clientRepository)
        {
            _chromeProfileRepository = chromeProfileRepository;
            _chromeProfileManager = chromeProfileManager;
            _clientRepository = clientRepository;
        }

        public ChromeProfileDto GetCurrentCProfile()
        {
            var cProfile = _chromeProfileRepository.FirstOrDefault();
            return ObjectMapper.Map<CChromeProfile.ChromeProfile, ChromeProfileDto>(cProfile);
        }

        public async Task CreateUpdateCProfile(CreateUpdateDto createUpdateDto)
        {
            if (_chromeProfileRepository.Any())
            {
                var cProfile = _chromeProfileRepository.FirstOrDefault();
                cProfile.Profile = createUpdateDto.Profile;

                await _chromeProfileRepository.UpdateAsync(cProfile);
            }
            else
            {
                var cProfile = _chromeProfileManager.CreateAsync(createUpdateDto.Profile);

                await _chromeProfileRepository.InsertAsync(cProfile);
            }
        }

        public bool IsAnyCProfile()
        {
            return _clientRepository.Any(x => x.ChromeProfile == GetCurrentCProfile().Profile);
        }
    }
}

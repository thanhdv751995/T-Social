using Extention.Management.ClientBelongToProfiles;
using Extention.Management.Clients;
using Extention.Management.Enums;
using Extention.Management.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Extention.Management.ClientBelongToProfile
{
    public class ClientBelongToProfileAppService : ManagementAppService, IClientBelongToProfileAppService
    {
        private readonly IClientBelongToProfileRepository _clientBelongToProfileRepository;
        private readonly ClientBelongToProfileManager _clientBelongToProfileManager;
        private readonly IClientRepository _clientRepository;
        private readonly IProfileClientRepository _profileClientRepository;

        public ClientBelongToProfileAppService(IClientBelongToProfileRepository clientBelongToProfileRepository,
            ClientBelongToProfileManager clientBelongToProfileManager,
            IClientRepository clientRepository,
            IProfileClientRepository profileClientRepository)
        {
            _clientBelongToProfileRepository = clientBelongToProfileRepository;
            _clientBelongToProfileManager = clientBelongToProfileManager;
            _clientRepository = clientRepository;
            _profileClientRepository = profileClientRepository;
        }

        public async Task CreateAsync(CreateClientBelongToProfileDto createClientBelongToProfileDto)
        {
            foreach (var CBTP in createClientBelongToProfileDto.ProfilesClientId)
            {
                var clientProfile = _clientBelongToProfileManager.CreateAsync(CBTP, createClientBelongToProfileDto.ClientId);

                await _clientBelongToProfileRepository.InsertAsync(clientProfile);
            }
        }
        public async Task CreateByListClientId(CreateProfileByListClientDto createProfileByListClientDto)
        {
            foreach (var client in createProfileByListClientDto.ListClientId)
            {
                foreach(var profile in createProfileByListClientDto.ProfileCLientId)
                {
                    var clientProfile = _clientBelongToProfileManager.CreateAsync(profile, client);

                    await _clientBelongToProfileRepository.InsertAsync(clientProfile);
                }    
            }
        }

        public async Task DeleteManyAsync(Guid[] ids)
        {
            await _clientBelongToProfileRepository.DeleteManyAsync(ids);
        }
        public async Task CreateProfileChecked(Guid ClientId, AddProfileCheckedDto addProfile)
        {
            var clientProfile = await _profileClientRepository.GetListAsync(x=>_clientBelongToProfileRepository.Any(cbp=>cbp.ProfileClientId == x.Id && cbp.ClientId == ClientId));
            if (addProfile.ListProfileName != null)
            {
                var add = addProfile.ListProfileName.Except(clientProfile.Select(x => x.ProfileName)).ToList();
                foreach (var addPro in add)
                {
                    var listProfileId = _profileClientRepository.Where(x => x.ProfileName == addPro).Select(x => x.Id);
                    foreach(var idProfile in listProfileId)
                    {
                        var id = _clientBelongToProfileManager.CreateAsync(idProfile, ClientId);
                        await _clientBelongToProfileRepository.InsertAsync(id);
                    }
                }

            }
        }
        public async Task DeleteProfileChecked(Guid ClientId, DeleteProfileCheckedDto deleteProfile)
        {
            if (deleteProfile.ListProfileName != null)
            {
                foreach (var deletePro in deleteProfile.ListProfileName)
                {
                    var listProfileId = _profileClientRepository.Where(x => x.ProfileName == deletePro).Select(x => x.Id);
                    foreach (var idProfile in listProfileId)
                    {
                        await _clientBelongToProfileRepository.DeleteAsync(x => x.ProfileClientId == idProfile && x.ClientId == ClientId);
                    }
                    
                }
            }
        }
        public async Task<List<ClientBelongToProfileDto>> GetClientBelongToProfile(Guid clientId)
        {
            var profiles = from client in _clientRepository.Where(x => x.Id == clientId)
                           join clientBelongToProfile in _clientBelongToProfileRepository on client.Id equals clientBelongToProfile.ClientId
                           join profile in _profileClientRepository on clientBelongToProfile.ProfileClientId equals profile.Id
                           select new { client, clientBelongToProfile, profile };

            var queryResult = await AsyncExecuter.ToListAsync(profiles);

            var profilesDtos = queryResult.Select(x =>
            {
                var profileDto = ObjectMapper.Map<ProfileClient, ClientBelongToProfileDto>(x.profile);

                profileDto.ProfileName = x.profile.ProfileName;

                return profileDto;
            }).ToList();

            return profilesDtos;
        }
    }
}

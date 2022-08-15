using Extention.Management.GroupTypes;
using Extention.Management.Profiles;
using Extention.Management.ScriptDefaultProfile;
using Extention.Management.ScriptDefaultProfiles;
using Extention.Management.ScriptDefaultTypes;
using Extention.Management.Scripts;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.ProfileGroupTypes
{
    public class ProfileGroupTypeAppService : ManagementAppService
    {
        private readonly IProfileGroupTypeRepository _profileGroupTypeRepository;
        private readonly ProfileGroupTypeManager _profileGroupTypeManager;
        private readonly IGroupTypeRepository _groupTypeRepository;
        private readonly IScriptDefaultTypeRepository _scriptDefaultTypeRepository;
        private readonly IScriptRepository _scriptRepository;
        private readonly ScriptDefaultProfileAppService _scriptDefaultProfileAppService;
        public ProfileGroupTypeAppService(IProfileGroupTypeRepository profileGroupTypeRepository,
            ProfileGroupTypeManager profileGroupTypeManager, IGroupTypeRepository groupTypeRepository,
            IScriptDefaultTypeRepository scriptDefaultTypeRepository,
            IScriptRepository scriptRepository,
            ScriptDefaultProfileAppService scriptDefaultProfileAppService)
        {
            _profileGroupTypeRepository = profileGroupTypeRepository;
            _profileGroupTypeManager = profileGroupTypeManager;
            _groupTypeRepository = groupTypeRepository;
            _scriptDefaultTypeRepository = scriptDefaultTypeRepository;
            _scriptRepository = scriptRepository;
            _scriptDefaultProfileAppService = scriptDefaultProfileAppService;
        }

        public async Task CreateAsync(CreateProfileGroupTypeDto createProfileGroupTypeDto)
        {
            foreach (var profileId in createProfileGroupTypeDto.ListProfileId)
            {
                foreach (var groupTypeId in createProfileGroupTypeDto.ListGroupTypeId)
                {
                    var profileGroupType = _profileGroupTypeManager.CreateAsync(profileId, groupTypeId);

                    await _profileGroupTypeRepository.InsertAsync(profileGroupType);
                }

            }
        }

        public async Task DeleteAsync(Guid Id)
        {
            await _profileGroupTypeRepository.DeleteAsync(Id);
        }
        public async Task DeleteMutilWithProfile(List<Guid> profileIds, List<string> listNameGroupType)
        {
            foreach (var nameGroup in listNameGroupType)
            {
                var listIdGroup = _groupTypeRepository.Where(x => x.Name == nameGroup).Select(x => x.Id);
                foreach (var id in listIdGroup)
                {
                    foreach (var idProfile in profileIds)
                    {
                        await _profileGroupTypeRepository.DeleteAsync(x => x.ProfileId == idProfile && x.GroupTypeId == id);
                    }
                }

                await CreateDeleteScriptDefault(profileIds, nameGroup, "DELETE");
            }
        }
        public async Task CreateMutilWithProfile(List<Guid> profileIds, List<string> listNameGroupType)
        {
            foreach (var nameGroup in listNameGroupType)
            {
                var listIdGroup = _groupTypeRepository.Where(x => x.Name == nameGroup).Select(x => x.Id);
                foreach (var id in listIdGroup)
                {
                    foreach (var idProfile in profileIds)
                    {
                        var group = _profileGroupTypeManager.CreateAsync(idProfile, id);
                        await _profileGroupTypeRepository.InsertAsync(group);
                    }
                }

                await CreateDeleteScriptDefault(profileIds, nameGroup, "CREATE");
            }
        }

        public async Task CreateDeleteScriptDefault(List<Guid> profileIds, string nameGroup, string action)
        {
            List<Script> listScript = new();

            if (action == "CREATE")
            {
                listScript = await _scriptRepository.GetListAsync(script => script.IsDefault
                && _groupTypeRepository.Any(gt => script.ScriptName.Contains(gt.Name)
                && gt.Name == nameGroup)
                && !_scriptDefaultTypeRepository.Any(sd => sd.ScriptId == script.Id));
            }
            else
            {
                listScript = await _scriptRepository.GetListAsync(script => script.IsDefault
                && _groupTypeRepository.Any(gt => script.ScriptName.Contains(gt.Name)
                && gt.Name == nameGroup)
                && _scriptDefaultTypeRepository.Any(sd => sd.ScriptId == script.Id));
            }

            if (listScript.Any())
            {
                foreach (var script in listScript)
                {
                    foreach (var idProfile in profileIds)
                    {
                        if(action == "CREATE")
                        {
                            await _scriptDefaultProfileAppService.CreateAsync(new CreateScriptDefaultTypeDto()
                            {
                                ScriptId = script.Id,
                                ProfileId = idProfile
                            });
                        }
                        else
                        {
                            await _scriptDefaultTypeRepository.DeleteAsync(x => x.ScriptId == script.Id && x.ProfileId == idProfile);
                        }
                    }
                }
            }
        }
    }
}

using Extention.Management.Enums;
using Extention.Management.Profiles;
using Extention.Management.ScriptDefaultProfiles;
using Extention.Management.ScriptDefaultTypes;
using Extention.Management.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Extention.Management.ScriptDefaultProfile
{
    public class ScriptDefaultProfileAppService : ManagementAppService, IScriptDefaultProfileAppService
    {
        private readonly IScriptDefaultTypeRepository _scriptDefaultTypeRepository;
        private readonly ScriptProfileTypeManager _scriptProfileTypeManager;
        private readonly IScriptRepository _scriptRepository;
        private readonly IProfileClientRepository _profileClientRepository;
        public ScriptDefaultProfileAppService(IScriptDefaultTypeRepository scriptDefaultTypeRepository,
            ScriptProfileTypeManager scriptProfileTypeManager,
            IScriptRepository scriptRepository,
            IProfileClientRepository profileClientRepository)
        {
            _scriptDefaultTypeRepository = scriptDefaultTypeRepository;
            _scriptProfileTypeManager = scriptProfileTypeManager;
            _scriptRepository = scriptRepository;
            _profileClientRepository = profileClientRepository;
        }

        public async Task CreateAsync(CreateScriptDefaultTypeDto createScriptDefaultTypeDto)
        {
            if (_scriptDefaultTypeRepository.Any(x => x.ScriptId == createScriptDefaultTypeDto.ScriptId && x.ProfileId == createScriptDefaultTypeDto.ProfileId))
            {
                throw new UserFriendlyException("Profile Already Exist");
            }

            var scriptDefaultType = _scriptProfileTypeManager.CreateAsync(createScriptDefaultTypeDto.ScriptId, createScriptDefaultTypeDto.ProfileId);

            await _scriptDefaultTypeRepository.InsertAsync(scriptDefaultType);
        }

        public async Task CreateByListAsync(CreateByListScriptDefaultTypeDto createByListScriptDefaultTypeDto)
        {
            List<Guid> profileIds;
            if (createByListScriptDefaultTypeDto.ProfileIds.Any(x => x.ToString() == "All"))
            {
                profileIds = _scriptRepository.Where(x=>_scriptDefaultTypeRepository.Any(y=>y.ScriptId == x.Id)).Select(pf=>pf.Id).ToList();
            }
            else
            {
                profileIds = createByListScriptDefaultTypeDto.ProfileIds;
            }

            foreach (var scriptDefaultType in profileIds)
            {
                CreateScriptDefaultTypeDto createScriptDefaultTypeDto = new()
                {
                    ScriptId = createByListScriptDefaultTypeDto.ScriptId,
                    ProfileId = scriptDefaultType
                };

                await CreateAsync(createScriptDefaultTypeDto);
            }
        }
        public async Task CreateByListScriptProfile(CreateByListScriptProfileDto createByListScriptDefaultTypeDto)
        {
            foreach (var profileId in createByListScriptDefaultTypeDto.ProfileIds)
            {
                foreach(var scriptId in createByListScriptDefaultTypeDto.ScriptIds)
                {
                    CreateScriptDefaultTypeDto createScriptDefaultTypeDto = new()
                    {
                        ScriptId =scriptId,
                        ProfileId = profileId
                    };

                    await CreateAsync(createScriptDefaultTypeDto);
                }    
            }
        }
       public async Task DeleteProfileByScript (Guid idScript, CreateDeleteProfileOfScriptDto listNameProfiles)
       {
            if (listNameProfiles.ListNameProfiles != null)
            {
                foreach (var name in listNameProfiles.ListNameProfiles)
                {
                    var listProfileId = _profileClientRepository.Where(x => x.ProfileName == name).Select(x => x.Id);
                    foreach (var idProfile in listProfileId)
                    {
                        await _scriptDefaultTypeRepository.DeleteAsync(x => x.ScriptId == idScript && x.ProfileId == idProfile);
                    }
                }
            }    
       }
        public async Task CreateProfileByScript(Guid idScript, CreateDeleteProfileOfScriptDto listNameProfiles)
        {
            if (listNameProfiles.ListNameProfiles != null)
            {
                foreach (var name in listNameProfiles.ListNameProfiles)
                {
                    var listProfileId = _profileClientRepository.Where(x => x.ProfileName == name).Select(x => x.Id);
                    foreach (var idProfile in listProfileId)
                    {
                        var id = _scriptProfileTypeManager.CreateAsync(idScript, idProfile);
                        await _scriptDefaultTypeRepository.InsertAsync(id);
                    }
                }
            }
        }
    }
}

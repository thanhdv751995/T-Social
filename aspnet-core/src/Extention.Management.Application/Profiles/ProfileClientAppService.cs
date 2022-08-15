using Extention.Management.ClientBelongToProfiles;
using Extention.Management.ClientInfomations;
using Extention.Management.Enums;
using Extention.Management.EStatusType;
using Extention.Management.ExtraProperties;
using Extention.Management.GroupTypes;
using Extention.Management.ProfileGroupTypes;
using Extention.Management.ScriptDefaultProfile;
using Extention.Management.ScriptDefaultProfiles;
using Extention.Management.ScriptDefaultTypes;
using Extention.Management.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;

namespace Extention.Management.Profiles
{
    public class ProfileClientAppService : ManagementAppService, IProfileClientAppService
    {
        private readonly IProfileClientRepository _profileClientRepository;
        private readonly ProfileClientManager _profileClientManager;
        private readonly IClientBelongToProfileRepository _clientBelongToProfileRepository;
        private readonly IScriptDefaultTypeRepository _scriptDefaultTypeRepository;
        private readonly IScriptRepository _scriptRepository;
        private readonly ScriptProfileTypeManager _scriptDefaultTypeManager;
        private readonly IGroupTypeRepository _groupTypeRepository;
        private readonly IProfileGroupTypeRepository _profileGroupTypeRepository;
        private readonly ProfileGroupTypeAppService _profileGroupTypeAppService;
        public ProfileClientAppService(
            IProfileClientRepository profileClientRepository,
            ProfileClientManager profileClientManager,
            IClientBelongToProfileRepository clientBelongToProfileRepository,
             IScriptDefaultTypeRepository scriptDefaultTypeRepository,
              IScriptRepository scriptRepository,
             ScriptProfileTypeManager scriptDefaultTypeManager,
             IGroupTypeRepository groupTypeRepository,
             IProfileGroupTypeRepository profileGroupTypeRepository,
             ProfileGroupTypeAppService profileGroupTypeAppService
            )
        {
            _profileClientRepository = profileClientRepository;
            _profileClientManager = profileClientManager;
            _clientBelongToProfileRepository = clientBelongToProfileRepository;
            _scriptDefaultTypeRepository = scriptDefaultTypeRepository;
            _scriptDefaultTypeRepository = scriptDefaultTypeRepository;
            _scriptRepository = scriptRepository;
            _scriptDefaultTypeManager = scriptDefaultTypeManager;
            _groupTypeRepository = groupTypeRepository;
            _profileGroupTypeRepository = profileGroupTypeRepository;
            _profileGroupTypeAppService = profileGroupTypeAppService;
        }
        public async Task<Guid> CreateAsync(int startTime, int duringMinutes, string profileName)
        {
            if (profileName.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException("Không được để trống tên hồ sơ");
            }
            else
            {
                var profile = _profileClientManager.CreateAsync(startTime, duringMinutes, profileName);

                await _profileClientRepository.InsertAsync(profile);
                return profile.Id;
            }
        }
        public async Task<List<Guid>> CreateProfileByListTime(CreateUpdateProfileClientDto input)
        {
            List<Guid> listId = new();
            foreach (var item in input.TimeValue)
            {
                var id = await CreateAsync(item.StartTime, item.DuringMinutes, input.ProfileName);
                listId.Add(id);
            }
            return listId;
        }
        public async Task<ProfileClientDto> GetAsync(Guid id)
        {
            var profile = await _profileClientRepository.GetAsync(id);

            return ObjectMapper.Map<ProfileClient, ProfileClientDto>(profile);
        }
        public async Task DeleteManyAsync(Guid[] ids)
        {
            await _profileClientRepository.DeleteManyAsync(ids);
        }
        public async Task DeleteAsync(string name)
        {
            var listIdProfile = _profileClientRepository.Where(x => x.ProfileName == name).Select(x => x.Id).ToList();
            foreach (var id in listIdProfile)
            {
                await _scriptDefaultTypeRepository.DeleteAsync(x => x.ProfileId == id);
                await _clientBelongToProfileRepository.DeleteAsync(x => x.ProfileClientId == id);
                await _profileClientRepository.DeleteAsync(id);
            }

        }

        public  List<ClientInfoWithStatus> GetList(Guid clientId, string profileName)
        {
            if (profileName.IsNullOrWhiteSpace() || profileName == "null")
            {
                profileName = "";
            }

            List<ClientInfoWithStatus> listClientInfoWithStatusMapDto = new();

            var listProfileClient = _profileClientRepository.Where(x => x.ProfileName.Contains(profileName)).Select(x => x.ProfileName).Distinct().ToList();

            foreach (var profileClient in listProfileClient)
            {
                ClientInfoWithStatus dto = new();
                dto.ProfileName = profileClient;
                dto.ListIdProfile = _profileClientRepository.Where(x => x.ProfileName == profileClient).Select(x => x.Id).ToList();
                dto.Status = _profileClientRepository.Any(x => x.ProfileName == profileClient && _clientBelongToProfileRepository.Any(cbp => cbp.ProfileClientId == x.Id
                && cbp.ClientId == clientId));

                listClientInfoWithStatusMapDto.Add(dto);
            }
            return listClientInfoWithStatusMapDto;
        }

        public async Task UpdateAsync(string profileName, UpdateMutilScriptProfileDto updateMutilScriptProfileDto)
        {
            var listId = _profileClientRepository.Where(x => x.ProfileName == profileName).Select(x => x.Id).ToList();
  
            var listIdDto = updateMutilScriptProfileDto.UpdateProfileDto.Where(x => x.IdProfile != "").Select(x => Guid.Parse(x.IdProfile)).ToList();

            var listIdDelete = listId.Except(listIdDto);

            var listScriptDefault = _scriptDefaultTypeRepository.Where(x => _profileClientRepository.Any(p => p.Id == x.ProfileId && p.ProfileName == profileName));

            var listScriptName = _scriptRepository.Where(x => listScriptDefault.Any(l => l.ScriptId == x.Id)).Select(x => x.ScriptName).Distinct().ToList();

            //var listScriptName = _scriptRepository.Where(x => x.Id == listScriptDefault.FirstOrDefault().ScriptId).Select(x => x.ScriptName).ToList();
            if(updateMutilScriptProfileDto.DeleteGroupTypeWithProfileDto.ListNameGroupType != null)
            {
                await _profileGroupTypeAppService.DeleteMutilWithProfile(listId, updateMutilScriptProfileDto.DeleteGroupTypeWithProfileDto.ListNameGroupType);
            }
            if(updateMutilScriptProfileDto.AddProfileWithGroupTypeDto.ListGroupTypeName != null)
            {
                await _profileGroupTypeAppService.CreateMutilWithProfile(listId, updateMutilScriptProfileDto.AddProfileWithGroupTypeDto.ListGroupTypeName);
            }
            if(updateMutilScriptProfileDto.ScriptDefaultName != null)
            {
                var listScriptDelete = listScriptName.Except(updateMutilScriptProfileDto.ScriptDefaultName);
                if (listScriptDelete != null)
                {
                    foreach (var name in listScriptDelete)
                    {
                        var listScriptDefaultId = _scriptDefaultTypeRepository.Where(x => _scriptRepository.Any(p => p.Id == x.ScriptId && p.ScriptName == name) && _profileClientRepository.Any(p => p.Id == x.ProfileId && p.ProfileName == profileName)).Select(x => x.Id).ToList();

                        await _scriptDefaultTypeRepository.DeleteManyAsync(listScriptDefaultId);
                    }
                }
                var listScriptAdd = updateMutilScriptProfileDto.ScriptDefaultName.Except(listScriptName);
                if (listScriptAdd != null)
                {
                    foreach (var script in listScriptAdd)
                    {
                        var listIdScript = _scriptRepository.Where(x => x.ScriptName == script).Select(x => x.Id);
                        foreach (var idScript in listIdScript)
                        {
                            foreach (var idProfile in listId)
                            {
                                var scriptDefaultTypeInsert = _scriptDefaultTypeManager.CreateAsync(idScript, idProfile);
                                await _scriptDefaultTypeRepository.InsertAsync(scriptDefaultTypeInsert);
                            }
                        }
                    }
                }
            }    
            if (listIdDelete != null)
            {
                foreach (var id in listIdDelete)
                {
                    await _scriptDefaultTypeRepository.DeleteAsync(x => x.ProfileId == id);
                    await _clientBelongToProfileRepository.DeleteAsync(x => x.ProfileClientId == id);
                    await _profileClientRepository.DeleteAsync(id);
                }
            }
            foreach (var item in updateMutilScriptProfileDto.UpdateProfileDto)
            {

                if (item.IdProfile == "")
                {
                    await CreateAsync(item.StartTime, item.DuringMinutes, profileName);
                }
                else
                {
                    var profile = await _profileClientRepository.GetAsync(Guid.Parse(item.IdProfile));
                    profile.ProfileName = profileName;
                    profile.StartTime = item.StartTime;
                    profile.DuringMinutes = item.DuringMinutes;
                    await _profileClientRepository.UpdateAsync(profile);
                }

            }


        }
            
        public async Task<CountProfileDto> GetListProfileWithScript(string profileName)
        {
            List<ProfileClient> listProfile = null;

            if (profileName.IsNullOrWhiteSpace() || profileName == "null")
            {
                profileName = "";
            }
            listProfile = await _profileClientRepository.GetListAsync(x => x.ProfileName.Contains(profileName));
            var listProfileDtoMap = ObjectMapper.Map<List<ProfileClient>, List<ClientProfileWithScriptDto>>(listProfile);

            foreach (var item in listProfileDtoMap)
            {
                var listScriptDefaultByProfileType = _scriptRepository.Where(script => _scriptDefaultTypeRepository.Any(scriptDefault => script.Id == scriptDefault.ScriptId
                && _profileClientRepository.Any(profile => profile.Id == scriptDefault.ProfileId) && scriptDefault.ProfileId == item.Id)).Select(x => x.ScriptName).ToList();
                item.ListScript = listScriptDefaultByProfileType;
                var listGroupTypeName = _groupTypeRepository.Where(group => _profileGroupTypeRepository.Any(pgt => pgt.GroupTypeId == group.Id && pgt.ProfileId == item.Id)).Select(x => x.Name).ToList();
                item.ListNameGroupType = listGroupTypeName;
            }

            var groupProfile = listProfileDtoMap.GroupBy(x => x.ProfileName);
            Dictionary<string, List<ClientProfileWithScriptDto>> groupDictionary = new();
            foreach (var group in groupProfile)
            {
                List<string> tempScript = new() { };
                List<string> tempGroupType = new() { };
                foreach (var item in group.ToList())
                {
                    item.ListScript = item.ListScript.Except(tempScript).ToList();
                    tempScript.AddRange(item.ListScript);
                    tempScript = tempScript.Distinct().ToList();

                    item.ListNameGroupType = item.ListNameGroupType.Except(tempGroupType).ToList();
                    tempGroupType.AddRange(item.ListNameGroupType);
                    tempGroupType = tempGroupType.Distinct().ToList();
                }
                groupDictionary.Add(group.Key, group.ToList());
            }
            return new CountProfileDto()
            {
                Count = listProfileDtoMap.Count,
                Result = groupDictionary
            };
        }
        public List<AddScriptWithProfileDto> GetListNameProfile(Guid? clientId)
        {
            List<AddScriptWithProfileDto> addScriptWithProfileDtos = new();
            List<string> listProfileClient = new();
            if (clientId == null)
            {
                 listProfileClient = _profileClientRepository.Select(x => x.ProfileName).Distinct().ToList();
            }    
            else
            {
                listProfileClient = _profileClientRepository.Where(x => _clientBelongToProfileRepository.Any(cbp => cbp.ClientId == clientId && x.Id == cbp.ProfileClientId)).Select(x => x.ProfileName).Distinct().ToList();
            }    
           

            foreach (var profileClient in listProfileClient)
            {
                AddScriptWithProfileDto addScriptWithProfileDto = new()
                {
                    NameProfile = profileClient,
                    ListIdProfile = _profileClientRepository.Where(x => x.ProfileName == profileClient).Select(x => x.Id).ToList()
                };

                addScriptWithProfileDtos.Add(addScriptWithProfileDto);
            }

            return addScriptWithProfileDtos;
        }
        public  List<AddScriptWithProfileDto> GetListProfileOfScript(Guid scriptId)
        {
            List<AddScriptWithProfileDto> addScriptWithProfileDtos = new();
            List<string> listProfileClient = new();
            listProfileClient = _profileClientRepository.Where(x => _scriptDefaultTypeRepository.Any(sdt => sdt.ScriptId == scriptId && x.Id == sdt.ProfileId)).Select(x => x.ProfileName).Distinct().ToList();

            foreach (var profileClient in listProfileClient)
            {
                AddScriptWithProfileDto addScriptWithProfileDto = new()
                {
                    NameProfile = profileClient,
                    ListIdProfile = _profileClientRepository.Where(x => x.ProfileName == profileClient).Select(x => x.Id).ToList()
                };

                addScriptWithProfileDtos.Add(addScriptWithProfileDto);
            }

            return addScriptWithProfileDtos;
        }
    }
}

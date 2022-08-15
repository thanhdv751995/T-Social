using Extention.Management.ProfileGroupTypes;
using Extention.Management.Profiles;
using Extention.Management.ScriptDefaultProfile;
using Extention.Management.ScriptDefaultProfiles;
using Extention.Management.ScriptDefaultTypes;
using Extention.Management.Scripts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Uow;

namespace Extention.Management.GroupTypes
{
    public class GroupTypeAppService : ManagementAppService
    {
        private readonly IGroupTypeRepository _groupTypeRepository;
        private readonly GroupTypeManager _groupTypeManager;
        private readonly ScriptAppService _scriptAppService;
        private readonly IConfiguration _configuration;
        private readonly IScriptRepository _scriptRepository;
        private readonly IScriptDefaultTypeRepository _scriptDefaultTypeRepository;
        private readonly ScriptDefaultProfileAppService _scriptDefaultProfileAppService;
        public GroupTypeAppService(IGroupTypeRepository groupTypeRepository,
            GroupTypeManager groupTypeManager,
            ScriptAppService scriptAppService,
            IConfiguration configuration,
            IScriptRepository scriptRepository,
            IScriptDefaultTypeRepository scriptDefaultTypeRepository,
            ScriptDefaultProfileAppService scriptDefaultProfileAppService)
        {
            _groupTypeRepository = groupTypeRepository;
            _groupTypeManager = groupTypeManager;
            _scriptAppService = scriptAppService;
            _configuration = configuration;
            _scriptRepository = scriptRepository;
            _scriptDefaultTypeRepository = scriptDefaultTypeRepository;
            _scriptDefaultProfileAppService = scriptDefaultProfileAppService;
        }

        public async Task<List<GroupTypeDto>> GetListAsync(string nameGroupType)
        {
            if (nameGroupType.IsNullOrWhiteSpace() || nameGroupType == "null")
            {
                nameGroupType = "";
            }
            var listGroupType = await _groupTypeRepository.GetListAsync(x => x.Name.Contains(nameGroupType));

            return ObjectMapper.Map<List<GroupType>, List<GroupTypeDto>>(listGroupType);
        }

        [UnitOfWork]
        public async Task CreateAsync(CreateUpdateGroupTypeDto createUpdateGroupTypeDto)
        {
            if (_groupTypeRepository.Any(x => x.Name == createUpdateGroupTypeDto.Name))
            {

                throw new UserFriendlyException($"Loại nhóm ${createUpdateGroupTypeDto.Name} đã tồn tại! Hãy nhập tên khác!");
            }

            var groupType = _groupTypeManager.CreateAsync(createUpdateGroupTypeDto.Name, createUpdateGroupTypeDto.KeywordsRelative);

            await _groupTypeRepository.InsertAsync(groupType);

            await CreateScriptByKeyword(createUpdateGroupTypeDto.KeywordsRelative, createUpdateGroupTypeDto.Name, Guid.Empty);
        }

        public async Task CreateScriptByKeyword(string keywordsRelative, string groupTypeName, Guid scriptId)
        {
            if (keywordsRelative.Contains(","))
            {
                var listKeywordRelative = keywordsRelative.Split(",");

                foreach (var keyword in listKeywordRelative)
                {
                    var script = await _scriptAppService.CreateHandlerAsync(new CreateUpdateScriptDto
                    {
                        ScriptName = $"JoinGroup With Keyword {keyword} GroupType {groupTypeName}",
                        IsActive = true,
                        IsDefault = true,
                        Value = $"1{_configuration.GetSection("FacebookParameter")["UrlCharacter"]}{keyword}",
                        Type = Type.Type.JoinGroup
                    });

                    if (scriptId != Guid.Empty)
                    {
                        var listProfileId = _scriptDefaultTypeRepository.Where(x => x.ScriptId == scriptId).Select(x => x.ProfileId).Distinct().ToList();

                        foreach (var profileId in listProfileId)
                        {
                            await _scriptDefaultProfileAppService.CreateAsync(new CreateScriptDefaultTypeDto() { ProfileId = profileId, ScriptId = script.Id });
                        }
                    }
                }
            }
            else
            {
                var script = await _scriptAppService.CreateHandlerAsync(new CreateUpdateScriptDto
                {
                    ScriptName = $"JoinGroup With Keyword {keywordsRelative} GroupType {groupTypeName}",
                    IsActive = true,
                    IsDefault = true,
                    Value = $"1{_configuration.GetSection("FacebookParameter")["UrlCharacter"]}{keywordsRelative}",
                    Type = Type.Type.JoinGroup
                });

                if (scriptId != Guid.Empty)
                {
                    var listProfileId = _scriptDefaultTypeRepository.Where(x => x.ScriptId == scriptId).Select(x => x.ProfileId).Distinct().ToList();

                    foreach (var profileId in listProfileId)
                    {
                        await _scriptDefaultProfileAppService.CreateAsync(new CreateScriptDefaultTypeDto() { ProfileId = profileId, ScriptId = script.Id });
                    }
                }
            }
        }

        public async Task UpdateAsync(Guid Id, CreateUpdateGroupTypeDto createUpdateGroupTypeDto)
        {
            var groupType = await _groupTypeRepository.GetAsync(Id);

            var keywordsRelativeFixed = createUpdateGroupTypeDto.KeywordsRelative.Split(",");

            var keywordsRelativeSplitCreate = createUpdateGroupTypeDto.KeywordsRelative.Split(",").ToList();

            keywordsRelativeSplitCreate.RemoveAll(x => groupType.KeywordsRelative.Contains(x));

            var KeywordsRelative = keywordsRelativeSplitCreate.JoinAsString(",");

            var listScript = await _scriptRepository.GetListAsync(x => x.ScriptName.Contains(groupType.Name));

            await CreateScriptByKeyword(KeywordsRelative, createUpdateGroupTypeDto.Name, listScript[0].Id);

            if (listScript.Any())
            {
                foreach (var script in listScript)
                {
                    if (!keywordsRelativeFixed.Any(x => script.Value.Contains(x)))
                    {
                        await _scriptRepository.DeleteAsync(script.Id);
                    }
                }
            }

            groupType.Name = createUpdateGroupTypeDto.Name;
            groupType.KeywordsRelative = createUpdateGroupTypeDto.KeywordsRelative;

            await _groupTypeRepository.UpdateAsync(groupType);
        }
        public async Task DeleteAsync(Guid id)
        {
            var groupType = await _groupTypeRepository.GetAsync(id);

            var keywordsRelative = groupType.KeywordsRelative.Split(",");

            foreach(var keyword in keywordsRelative)
            {
                var script = await _scriptRepository.GetAsync(x => x.ScriptName.Contains(groupType.Name) && x.ScriptName.Contains(keyword));

                await _scriptRepository.DeleteAsync(script.Id);
            }

            await _groupTypeRepository.DeleteAsync(id);
        }
    }
}

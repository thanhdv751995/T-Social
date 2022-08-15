using Extention.Management.ExtensionVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.ExtensionVariable
{
    public class ExtensionVariableAppService : ManagementAppService
    {
        private readonly IExtensionVariableRepository _extensionVariableRepository;
        private readonly ExtensionVariableManager _extensionVariableManager;
        public ExtensionVariableAppService(IExtensionVariableRepository extensionVariableRepository,
            ExtensionVariableManager extensionVariableManager)
        {
            _extensionVariableRepository = extensionVariableRepository;
            _extensionVariableManager = extensionVariableManager;
        }

        public async Task CreateAsync(ExtensionVariableCreateDto extensionVariableCreateDto)
        {
            var extensionVariable = _extensionVariableManager.CreateAsync(extensionVariableCreateDto.ClassName, extensionVariableCreateDto.Value);

            await _extensionVariableRepository.InsertAsync(extensionVariable);
        }

        public async Task UpdateAsync(ExtensionVariableCreateDto extensionVariableCreateDto)
        {
            var extension = await _extensionVariableRepository.GetAsync(x => x.ClassName == extensionVariableCreateDto.ClassName);

            extension.Value = extensionVariableCreateDto.Value;

            await _extensionVariableRepository.UpdateAsync(extension);
        }

        public async Task<List<ExtensionVariableDto>> GetListAsync()
        {
            var listExtensionVariable = await _extensionVariableRepository.GetListAsync();

            return ObjectMapper.Map<List<ExtensionVariables.ExtensionVariable>, List <ExtensionVariableDto>>(listExtensionVariable);
        }
    }
}

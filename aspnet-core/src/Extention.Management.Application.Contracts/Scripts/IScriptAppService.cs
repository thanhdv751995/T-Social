using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Extention.Management.Scripts
{
    public interface IScriptAppService : IApplicationService
    {
        Task UpdateActive(UpdateActiveScriptDto updateActiveScriptDto);
        Task DeleteScriptAsync(Guid Id);
        Task<ScriptDto> GetAsync(Guid id);
        Task<ScriptDto> CreateAsync(CreateUpdateScriptDto input);
        Task SheduleOrEnqueueCreateScriptJobAsync(Guid scriptId, int times, List<Guid> clientsIdLikePost, Type.Type type);
        Task<ScriptDto> CreateHandlerAsync(CreateUpdateScriptDto input);
    }
}

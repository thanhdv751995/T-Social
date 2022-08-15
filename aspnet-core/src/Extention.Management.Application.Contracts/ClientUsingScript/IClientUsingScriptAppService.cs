using Extention.Management.Scripts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Extention.Management.ProxyUsingScript
{
    public interface IClientUsingScriptAppService : IApplicationService
    {
        Task<PagedResultDto<ClientUsingScriptDto>> GetListAsync(string clientId,string seedingName, string scriptName, string type, string isActive, int skip, int take);
        Task<ClientUsingScriptDto> CreateAsync(CreateClientUsingScriptDto createClientUsingScriptDto);
        Task UpdateErrorDetail(string scriptId, string clientId);
        Task DeleteAsync(Guid Id);
        Task<ClientUsingScriptDto> GetFirstClientUsingScriptDefaultAsync(string username);
        Task UpdateActiveByScriptId(UpdatePUSByClientProxyIpDto updatePUSByScriptProxyIpDto);
        Task RepeatOrRevokePUSDefault(bool isActive, Guid[] clientIds);
        Task CreateByListScriptDefaultAsync(Guid clientId);
        Task UpdateActiveManyScript(Guid clientId, bool isActive);
        Task InvokeNewScript(ClientUsingScriptDto clientUsingScriptDto);
        Task ScheduleNewScript(UpdatePUSByClientProxyIpDto updatePUSByScriptClientIpDto);
        Task ChangeClientJob(UpdatePUSByClientProxyIpDto updatePUSByScriptClientIpDto);
        Task InvokeNewDefaultScriptAsync(Guid clientId);
    }
}

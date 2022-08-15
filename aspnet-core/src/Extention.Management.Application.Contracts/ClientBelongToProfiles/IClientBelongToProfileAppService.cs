using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Extention.Management.ClientBelongToProfiles
{
    public interface IClientBelongToProfileAppService : IApplicationService
    {
        Task CreateAsync(CreateClientBelongToProfileDto createClientBelongToProfileDto);
        Task DeleteManyAsync(Guid[] ids);
        Task<List<ClientBelongToProfileDto>> GetClientBelongToProfile(Guid clientId);
    }
}

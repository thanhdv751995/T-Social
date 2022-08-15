using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Extention.Management.Profiles
{
    public interface IProfileClientAppService : IApplicationService
    {
        Task<List<Guid>> CreateProfileByListTime(CreateUpdateProfileClientDto input);
        Task<ProfileClientDto> GetAsync(Guid id);
    }
}

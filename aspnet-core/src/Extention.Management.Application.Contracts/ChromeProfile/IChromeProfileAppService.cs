using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Extention.Management.ChromeProfile
{
    public interface IChromeProfileAppService : IApplicationService
    {
        ChromeProfileDto GetCurrentCProfile();
        Task CreateUpdateCProfile(CreateUpdateDto createUpdateDto);
    }
}

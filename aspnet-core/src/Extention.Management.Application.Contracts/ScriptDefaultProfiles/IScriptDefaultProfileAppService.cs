using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Extention.Management.ScriptDefaultProfiles
{
    public interface IScriptDefaultProfileAppService : IApplicationService
    {
        Task CreateAsync(CreateScriptDefaultTypeDto createScriptDefaultTypeDto);
        //Task CreateByListAsync(CreateByListScriptDefaultTypeDto createByListScriptDefaultTypeDto);
    }
}

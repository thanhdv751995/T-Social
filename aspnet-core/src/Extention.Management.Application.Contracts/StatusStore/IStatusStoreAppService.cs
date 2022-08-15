using Extention.Management.EStatusType;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Extention.Management.StatusStore
{
    public interface IStatusStoreAppService: IApplicationService
    {
        Task CreateAsync(CreateStatusStoreDto createStatusStoreDto);
        Task<StatusStoreDto> GetStatusRandomAsync(StatusType statusType);
        List<object> GetEnumsType();
    }
}

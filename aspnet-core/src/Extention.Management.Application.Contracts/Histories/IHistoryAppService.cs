using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Extention.Management.Histories
{
    public interface IHistoryAppService : IApplicationService
    {
        Task CreateAsync(CreateDto createDto);
        Task UpdateTimeStart(UpdateDateTimeDto updateDateTimeDto);
        Task UpdateTimeEnd(UpdateDateTimeDto updateDateTimeDto);
        List<HistoryDto> GetListAsync(int skip, int take, string clientId);
        Task DeleteManyAsync(Guid scriptId);
    }
}

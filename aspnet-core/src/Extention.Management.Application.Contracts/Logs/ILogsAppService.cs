using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Extention.Management.Logs
{
    public interface ILogsAppService : IApplicationService
    {
        Task CreateLogAsync(CreateProxyClientLogDto proxyClientLogDto);
        Task<List<ProxyCientLogDto>> GetListLogs(string IP, string date, int skip, int take);
    }
}

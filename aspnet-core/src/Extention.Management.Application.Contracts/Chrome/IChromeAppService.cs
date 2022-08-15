using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Extention.Management.Chrome
{
    public interface IChromeAppService
    {
        Task StartChromeAsync(string profile, string url);
        Task NewChromeProfileAsync(string profile, string url);
    }
}

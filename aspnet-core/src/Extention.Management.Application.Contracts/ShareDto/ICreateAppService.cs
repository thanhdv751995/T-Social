using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Extention.Management.ShareDto
{
    public interface ICreateAppService : IApplicationService
    {
        Task CreateAsync<T>(T t);
    }
}

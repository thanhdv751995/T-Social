using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Extention.Management.Enums
{
    public interface IEnumAppService : IApplicationService
    {
        List<object> GetListProfileType();
        List<object> GetListType();
        List<object> GetListStatusType();
    }
}

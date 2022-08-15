using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Extention.Management.BackgroundJob
{
    public interface IBackgroundJobAppService : IApplicationService
    {
        void EnqueueJob<T>(Expression<Action<T>> action);
        Task CreateUpdateClientDailyJob();
        Task InvokeCloseChromeBackgroundJob();
        Task InvokeNewScriptClientUsing();
        Task CheckExceptionCloseChrome();
    }
}

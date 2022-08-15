using Extention.Management.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Extention.Management.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class ManagementController : AbpController
    {
        protected ManagementController()
        {
            LocalizationResource = typeof(ManagementResource);
        }
    }
}
using ActiviteChart.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ActiviteChart.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class ActiviteChartController : AbpController
    {
        protected ActiviteChartController()
        {
            LocalizationResource = typeof(ActiviteChartResource);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using ActiviteChart.Localization;
using Volo.Abp.Application.Services;

namespace ActiviteChart
{
    /* Inherit your application services from this class.
     */
    public abstract class ActiviteChartAppService : ApplicationService
    {
        protected ActiviteChartAppService()
        {
            LocalizationResource = typeof(ActiviteChartResource);
        }
    }
}

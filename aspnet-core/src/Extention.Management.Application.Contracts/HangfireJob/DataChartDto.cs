using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.HangfireJob
{
    public class DataChartDto
    {
        public List<string> labels { get; set; }
        public List<DatasetDto> datasets { get; set; }
    }
}

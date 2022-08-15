using System;
using System.Collections.Generic;
using System.Text;

namespace ActiviteChart.ActiviteChartDatas
{
    public class DatasetChartActivitiesDto
    {
        public List<int> labels { get; set; }
        public List<DatasetDto> datasets { get; set; }
    }
}

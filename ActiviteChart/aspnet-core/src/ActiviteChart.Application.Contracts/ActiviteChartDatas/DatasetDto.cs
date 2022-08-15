using System;
using System.Collections.Generic;
using System.Text;

namespace ActiviteChart.ActiviteChartDatas
{
    public class DatasetDto
    {
        public string label { get; set; }
        public List<int> data { get; set; }
        public string backgroundColor { get; set; }
        public string stack { get; set; } = "Stack 0";
    }
}

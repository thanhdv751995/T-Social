using System;
using System.Collections.Generic;
using System.Text;

namespace ActiviteChart.ActiviteChartDatas
{
    public class CreateActiviteChartDataDto
    {
        public string Username { get; set; }
        public Guid ScriptId { get; set; }
        public string TypeName { get; set; }
        public string Content { get; set; }
    }
}

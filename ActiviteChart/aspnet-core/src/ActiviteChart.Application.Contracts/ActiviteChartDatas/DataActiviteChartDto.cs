using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.KafkaProducer
{
    public class DataActiviteChartDto
    {
        public string Username { get; set; }
        public Guid ScriptId { get; set; }
        public string TypeName { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
    }
}

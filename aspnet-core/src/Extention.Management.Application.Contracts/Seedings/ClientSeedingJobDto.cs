using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Seedings
{
    public class ClientSeedingJobDto
    {
        public string ClientName { get; set; }
        public string ClientId { get; set; }
        public string TypeSeeding { get; set; }
        public string Value { get; set; }
        public bool Status { get; set; }
        public DateTime CreationTime { get; set; }
    }
}

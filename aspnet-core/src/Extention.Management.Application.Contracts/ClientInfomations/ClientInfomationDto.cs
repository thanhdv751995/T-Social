using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ClientInfomations
{
    public class ClientInfomationDto
    {
        public Guid IdUser { get; set; }
        public string ClientId { get; set; }
        public string NameUser { get; set; }
        public string DayOfBirth { get; set; }
    }
}

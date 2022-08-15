using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Statuss
{
    public class CreateUpdateStatusDto
    {
        public Guid IdUser { get; set; }
        public string Content { get; set; }
    }
}

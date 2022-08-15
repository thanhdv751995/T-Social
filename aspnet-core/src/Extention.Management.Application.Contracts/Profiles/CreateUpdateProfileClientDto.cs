
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Extention.Management.Profiles
{
    public class CreateUpdateProfileClientDto
    {
        public List<TimeValueProfileDto> TimeValue { get; set; }
        public string ProfileName { get; set; }
    }
}

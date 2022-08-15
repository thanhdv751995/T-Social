using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Profiles
{
    public class UpdateProfileDto
    {
        public string IdProfile { get; set; }
        public int StartTime { get; set; }
        public int DuringMinutes { get; set; }
    }
}

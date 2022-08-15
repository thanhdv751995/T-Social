
using Extention.Management.ExtraProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Profiles
{
    public class ClientProfileWithScriptDto
    {
        public Guid Id { get; set; }
        public int StartTime { get; set; }
        public int DuringMinutes { get; set; }
        public string ProfileName { get; set; }
        public List<string> ListScript { get; set; }
        public List<string> ListNameGroupType { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.Profiles
{
    public class ProfileClient : AuditedAggregateRoot<Guid>
    {
        public int StartTime { get; set; }
        public int DuringMinutes { get; set; }
        public string ProfileName { get; set; }
        private ProfileClient()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal ProfileClient(
               Guid id,
               [NotNull] int startTime,
               [NotNull] int duringMinutes,
               [NotNull] string profileName
           )
           : base(id)
        {
            StartTime = startTime;
            DuringMinutes = duringMinutes;
            ProfileName = profileName;
        }
    }
}

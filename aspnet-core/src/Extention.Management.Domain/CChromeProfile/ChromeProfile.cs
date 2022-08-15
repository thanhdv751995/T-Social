using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.CChromeProfile
{
    public class ChromeProfile : AuditedAggregateRoot<Guid>
    {
        public string Profile { get; set; }
        private ChromeProfile()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal ChromeProfile(Guid id,
            string profile) : base(id)
        {
            Profile = profile;
        }
    }
}

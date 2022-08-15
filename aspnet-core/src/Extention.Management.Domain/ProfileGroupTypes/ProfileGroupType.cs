using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.ProfileGroupTypes
{
    public class ProfileGroupType : AuditedAggregateRoot<Guid>
    {
        public Guid ProfileId { get; set; }
        public Guid GroupTypeId { get; set; }
        private ProfileGroupType()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal ProfileGroupType(
               Guid id,
               [NotNull] Guid profileId,
               [NotNull] Guid groupTypeId
           )
           : base(id)
        {
            ProfileId = profileId;
            GroupTypeId = groupTypeId;
        }
    }
}

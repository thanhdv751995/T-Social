using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.ProfileGroupTypes
{
    public class ProfileGroupTypeManager : DomainService
    {
        public ProfileGroupTypeManager()
        {

        }

        public ProfileGroupType CreateAsync(
            [NotNull] Guid profileId,
            [NotNull] Guid groupTypeId
            )
        {
            return new ProfileGroupType(
                GuidGenerator.Create(),
                profileId,
                groupTypeId
            );
        }
    }
}

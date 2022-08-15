
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.Profiles
{
    public class ProfileClientManager : DomainService
    {
        public ProfileClientManager()
        {

        }

        public ProfileClient CreateAsync(
            [NotNull] int startTime,
            [NotNull] int duringMinutes,
            [NotNull] string profileName
            )
        {
            return new ProfileClient(
                GuidGenerator.Create(),
                startTime,
                duringMinutes,
                profileName
            );
        }
    }
}

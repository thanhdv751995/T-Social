using Extention.Management.ProfileOfClients;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.ClientBelongToProfiles
{
    public class ClientBelongToProfileManager : DomainService
    {
        public ClientBelongToProfileManager()
        {

        }

        public ClientBelongToProfile CreateAsync(
            [NotNull] Guid profileClientId,
            [NotNull] Guid ClientId
            )
        {
            return new ClientBelongToProfile(
                GuidGenerator.Create(),
                profileClientId,
                ClientId
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.ClientFriends
{
    public class ClientFriendManager : DomainService
    {
        public ClientFriendManager()
        {
        }
        public ClientFriend CreateAsync(
           [NotNull] Guid idUser,
           [NotNull] string userName,
           [NotNull] string friendName,
           [NotNull] string avatarUrl
           )
        {
            return new ClientFriend(
                GuidGenerator.Create(),
                idUser,
                userName,
                friendName,
                avatarUrl
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.ClientFriends
{
    public class ClientFriend : AuditedAggregateRoot<Guid>
    {
        public Guid IdUser { get; set; }
        public string UserName { get; set; }
        public string FriendName { get; set; }
        public string AvatarUrl { get; set; }
        private ClientFriend()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal ClientFriend(
               Guid id,
               [NotNull] Guid idUser,
               [NotNull] string userName,
               [NotNull] string friendName,
                string avatarUrl
           )
           : base(id)
        {
            IdUser = idUser;
            UserName = userName;
            FriendName = friendName;
            AvatarUrl = avatarUrl;
        }
    }
}

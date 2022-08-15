using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ClientFriends
{
    public class CreateClientFriendDto
    {
        public Guid IdUser { get; set; }
        public string UserName { get; set; }
        public string FriendName { get; set; }
        public string AvatarUrl { get; set; }
    }
}

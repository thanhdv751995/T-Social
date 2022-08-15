using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ClientFriends
{
    public class FriendDto
    {
        public Guid IdUser { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public string UserName { get; set; }
        public DateTime TimeAdd { get; set; }
        public int MutualFriends { get; set; }

    }
}

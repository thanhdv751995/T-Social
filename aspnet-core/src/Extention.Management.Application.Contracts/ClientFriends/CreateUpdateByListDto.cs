using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ClientFriends
{
    public class CreateUpdateByListDto
    {
        public Guid IdUser { get; set; }
        public List<CreateClientFriendDto> CreateClientFriendDtos { get; set; }
    }
}

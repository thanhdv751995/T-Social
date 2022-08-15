using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.GroupJoin
{
    public class GroupJoinDto
    {
        public string Username { get; set; }
        public string GroupName { get; set; }
        public string AvatarGroup { get; set; }
        public string GroupURL { get; set; }
        public string Content { get; set; }
        public Guid GroupTypeId { get; set; }
    }
}

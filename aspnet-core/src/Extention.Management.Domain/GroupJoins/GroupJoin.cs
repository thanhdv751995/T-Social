using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.GroupJoins
{
    public class GroupJoin : AuditedAggregateRoot<Guid>
    {
        public string Username { get; set; }
        public string GroupName { get; set; }
        public string AvatarGroup { get; set; }
        public string GroupURL { get; set; }
        public string Content { get; set; }
        public Guid GroupTypeId { get; set; }
        private GroupJoin()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal GroupJoin(
               Guid id,
               string userName,
               string groupName,
               string avatarGroup,
               string groupURL,
               string content,
               Guid groupTypeId
           )
           : base(id)
        {
            Username = userName;
            GroupName = groupName;
            AvatarGroup = avatarGroup;
            GroupURL = groupURL;
            Content = content;
            GroupTypeId = groupTypeId;
        }
    }
}

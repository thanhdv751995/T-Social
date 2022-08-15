using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.GroupJoins
{
    public class GroupJoinManager : DomainService
    {
        public GroupJoinManager()
        {

        }

        public GroupJoin CreateAsync(
               string userName,
               string groupName,
               string avatarGroup,
               string groupURL,
               string content,
               Guid groupTypeId
            )
        {
            return new GroupJoin(
                GuidGenerator.Create(),
                   userName,
                   groupName,
                   avatarGroup,
                   groupURL,
                   content,
                   groupTypeId
            );
        }
    }
}

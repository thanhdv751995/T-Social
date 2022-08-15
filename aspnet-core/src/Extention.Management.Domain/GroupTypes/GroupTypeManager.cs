using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.GroupTypes
{
    public class GroupTypeManager : DomainService
    {
        public GroupTypeManager()
        {

        }

        public GroupType CreateAsync(
               string name,
               string keywordsRelative
            )
        {
            return new GroupType(
                GuidGenerator.Create(),
                   name,
                   keywordsRelative
            );
        }
    }
}


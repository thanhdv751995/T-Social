using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.GroupTypes
{
    public class GroupType : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string KeywordsRelative { get; set; }
        private GroupType()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal GroupType(
               Guid id,
               string name,
               string keywordsRelative
           )
           : base(id)
        {
            Name = name;
            KeywordsRelative = keywordsRelative;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.ScriptDefaultTypes
{
    public class ScriptDefaultType : AuditedAggregateRoot<Guid>
    {
        public Guid ScriptId { get; set; }
        public Guid ProfileId { get; set; }
        public ScriptDefaultType()
        {

        }

        internal ScriptDefaultType(
               Guid id,
               Guid scriptId,
               Guid profileId
           )
           : base(id)
        {
            ScriptId = scriptId;
            ProfileId = profileId;         
        }
    }
}

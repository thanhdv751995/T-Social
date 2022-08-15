using Extention.Management.ScriptTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.ExpandScripts
{
    public class ExpandScript : AuditedAggregateRoot<Guid>
    {
        public Guid ScriptId { get; set; }
        public bool IsActive { get; set; }
        public ScriptTypeEnum ScriptType { get; set; }
        public string URL { get; set; }
        private ExpandScript()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal ExpandScript(
               Guid id,
               [NotNull] Guid scripId,
               [NotNull] bool isActive,
               [NotNull] ScriptTypeEnum scriptType,
               string url
           )
           : base(id)
        {
            ScriptId = scripId;
            IsActive = isActive;
            ScriptType = scriptType;
            URL = url;
        }
    }
}

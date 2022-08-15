using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.Scripts
{
    public class Script : AuditedAggregateRoot<Guid>
    {
        public string ScriptName { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public Type.Type Type { get; set; }
        private Script()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal Script(
               Guid id,
               [NotNull] string scriptName,
               [NotNull] bool isActive,
               [NotNull] bool isDefault,
               [NotNull] string value,
               [NotNull] Type.Type type
           )
           : base(id)
        {
            ScriptName = scriptName;
            IsActive = isActive;
            IsDefault = isDefault;
            Value = value;
            Type = type;
        }
    }
}

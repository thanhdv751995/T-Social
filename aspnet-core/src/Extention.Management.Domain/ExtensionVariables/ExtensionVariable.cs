using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.ExtensionVariables
{
    public class ExtensionVariable : AuditedAggregateRoot<Guid>
    {
        public string ClassName { get; set; }
        public string Value { get; set; }

        private ExtensionVariable()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal ExtensionVariable(
               Guid id,
               [NotNull] string className,
               [NotNull] string value
           )
           : base(id)
        {
            ClassName = className;
            Value = value;
        }
    }
}

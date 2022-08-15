using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.Statuss
{
    public class Status : AuditedAggregateRoot<Guid>
    {
        public Guid IdUser { get; set; }
        public string Content { get; set; }
        private Status()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal Status(
               Guid id,
               [NotNull] Guid idUSer,
               string content
           )
           : base(id)
        {
            IdUser = idUSer;
            Content = content;
        }
    }
}

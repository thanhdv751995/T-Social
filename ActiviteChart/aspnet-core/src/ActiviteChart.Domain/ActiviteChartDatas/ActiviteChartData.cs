using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace ActiviteChart.ActiviteChartDatas
{
    public class ActiviteChartData : AuditedAggregateRoot<Guid>
    {
        public string Username { get; set; }
        public Guid ScriptId { get; set; }
        public string TypeName { get; set; }
        public string Content { get; set; }
        private ActiviteChartData()
        {
        }
        internal ActiviteChartData(
                 Guid id,
                 [NotNull] string userName,
                 [NotNull] Guid scriptId,
                 [NotNull] string typeName,
                 [NotNull] string content
                )
                : base(id)
        {
            Username = userName;
            ScriptId = scriptId;
            TypeName = typeName;
            Content = content;
        }
    }
}

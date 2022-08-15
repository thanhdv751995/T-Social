using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.HangfireJobs
{
    public class HangfireJob : AuditedAggregateRoot<Guid>
    {
        public Guid JobId { get; set; }
        public Guid ClientId { get; set; }
        public Guid ScriptId { get; set; }
        public bool IsFinish { get; set; }
        private HangfireJob()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal HangfireJob(
            Guid id,
            Guid jobId,
            Guid clientId,
            Guid scriptId,
            bool isFinish
            ) : base(id)
        {
            JobId = jobId;
            ClientId = clientId;
            ScriptId = scriptId;
            IsFinish = isFinish;
        }
    }
}

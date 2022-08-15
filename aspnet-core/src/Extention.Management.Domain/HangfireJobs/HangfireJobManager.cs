using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.HangfireJobs
{
    public class HangfireJobManager : DomainService
    {
        public HangfireJobManager()
        {

        }

        public HangfireJob CreateAsync(
               Guid jobId,
               Guid clientId,
               Guid scriptId,
               bool isFinish
            )
        {
            return new HangfireJob(
                GuidGenerator.Create(),
                   jobId,
                   clientId,
                   scriptId,
                   isFinish
            );
        }
    }
}

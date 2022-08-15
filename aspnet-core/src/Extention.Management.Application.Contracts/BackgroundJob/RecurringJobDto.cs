using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.BackgroundJob
{
    public class RecurringJobDto
    {
        public string JobId { get; set; }
        public string State { get; set; }
        public string Error { get; set; }
        public string LastJobState { get; set; }
        public DateTime? LastExecution { get; set; }
        public DateTime? NextExecution { get; set; }
        public string Cron { get;set; }
    }
}

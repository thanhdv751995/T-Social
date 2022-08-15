using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Histories
{
    public class HistoryDto : AuditedEntityDto<Guid>
    {
        public Guid ClientId { get; set; }
        public string UserName { get; set; }
        public string ProxyIp { get; set; }
        public Guid ScriptId { get; set; }
        public string ScriptName { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
    }
}

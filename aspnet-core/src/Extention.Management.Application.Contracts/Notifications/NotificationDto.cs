using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Notifications
{
    public class NotificationDto : AuditedEntityDto<Guid>
    {
        public string IdUser { get; set; }
        public string Content { get; set; }
        public string UrlAvatar { get; set; }
        public string Time { get; set; }
        public string Href { get; set; }
    }
}

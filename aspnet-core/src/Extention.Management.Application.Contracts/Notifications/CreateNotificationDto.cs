using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Notifications
{
    public class CreateNotificationDto
    {
        public string IdUser { get; set; }
        public string Content { get; set; }
        public string UrlAvatar { get; set; }
        public string Time { get; set; }
        public string Href { get; set; }
    }
}

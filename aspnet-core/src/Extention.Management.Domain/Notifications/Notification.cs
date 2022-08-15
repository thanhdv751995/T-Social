using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.Notifications
{
    public class Notification : AuditedAggregateRoot<Guid>
    {
        public string IdUser { get; set; }
        public string Content { get; set; }
        public string UrlAvatar { get; set; }
        public string Time { get; set; }
        public string Href { get; set; }
        private Notification()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal Notification(
               Guid id,
               string idUser,
               [NotNull] string content,
               string urlAvatar,
               string time,
               string href
           )
           : base(id)
        {
            IdUser = idUser;
            Content = content;
            UrlAvatar = urlAvatar;
            Time = time;
            Href = href;
        }
    }
}

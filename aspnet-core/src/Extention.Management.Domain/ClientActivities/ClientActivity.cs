using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.ClientActivities
{
    public class ClientActivity : AuditedAggregateRoot<Guid>
    {
        public string UserName { get; set; }
        public string Content { get; set; }
        public string URL { get; set; }
        public string ScriptName { get; set; }
        private ClientActivity()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal ClientActivity(
               Guid id,
               [NotNull] string userName,
               [NotNull] string content,
               string url,
               string scriptName
           )
           : base(id)
        {
            UserName = userName;
            Content = content;
            URL = url;
            ScriptName = scriptName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Extention.Management.Clients
{
    public class Client : AuditedAggregateRoot<Guid>
    {
        public string NameFacebook { get; set; }
        public string AvatarUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SecretKey { get; set; }
        public string Cookie { get; set; }
        public bool IsActive { get; set; }
        public string ProxyIp { get; set; }
        public string AccessToken { get; set; }
        public string ChromeProfile { get; set; }
        public string ComputerName { get; set; }
        public bool Online { get; set; }
        public bool HasCheckPoint { get; set; }
        private Client()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal Client(
               Guid id,
               [NotNull] string nameFacebook,
               [NotNull] string avatarUrl,
               [NotNull] string userName,
               [NotNull] string password,
               [NotNull] string secretKey,
               [NotNull] string cookie,
               [NotNull] bool isActive,
                string proxyIp,
                [NotNull] string accessToken,
                string chromeProfile,
                string computerName,
                [NotNull] bool online,
                bool hasCheckPoint
           )
           : base(id)
        {
            NameFacebook = nameFacebook;
            AvatarUrl = avatarUrl;
            UserName = userName;
            Password = password;
            SecretKey = secretKey;
            Cookie = cookie;
            IsActive = isActive;
            ProxyIp = proxyIp;
            AccessToken = accessToken;
            ChromeProfile = chromeProfile;
            ComputerName = computerName;
            Online = online;
            HasCheckPoint = hasCheckPoint;
        }
    }
}

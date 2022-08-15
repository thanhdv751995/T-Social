using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.IdentityServer.Clients;

namespace Extention.Management.Clients
{
    public class ClientManager : DomainService
    {
        public ClientManager()
        {
        }
        public Client CreateAsync(
           [NotNull] string nameFacebook,
           string avatarUrl,
           [NotNull] string UserName,
           [NotNull] string password,
            string secretKey,
            string cookie,
           [NotNull] bool isActive,
            string proxyIp,
            string accessToken,
            string chromeProfile,
            string computerName,
            [NotNull] bool online,
            bool hasCheckPoint
           )
        {
            return new Client(
                GuidGenerator.Create(),
                nameFacebook,
                avatarUrl,
                UserName,
                password,
                secretKey,
                cookie,
                isActive,
                proxyIp,
                accessToken,
                chromeProfile,
                computerName,
                online,
                hasCheckPoint
            );
        }
    }

}

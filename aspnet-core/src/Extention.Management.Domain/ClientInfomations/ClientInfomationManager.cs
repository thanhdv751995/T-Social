using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.ClientInfomations
{
    public class ClientInfomationManager : DomainService
    {
        public ClientInfomationManager()
        {
        }
        public ClientInfomation CreateAsync(
           [NotNull] Guid idUser,
           [NotNull] string clientId,
           [NotNull] string nameUser,
            string dayOfBirth
           )
        {
            return new ClientInfomation(
                GuidGenerator.Create(),
                idUser,
                clientId,
                nameUser,
                dayOfBirth
            );
        }
    }
}

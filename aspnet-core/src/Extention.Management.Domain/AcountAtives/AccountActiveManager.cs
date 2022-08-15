using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.AcountAtives
{
    public class AccountAtiveManager :  DomainService
    {
        public AccountActive CreateAsync(
          [NotNull] Guid proxyId,
          [NotNull] Guid accountId

          )
        {
            return new AccountActive(
                GuidGenerator.Create(),
                proxyId,
                accountId
            );
        }
    }
}

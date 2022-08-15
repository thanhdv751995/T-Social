using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.AccountUsingScripts
{
    public class ClientUsingScriptManager : DomainService
    {
        public ClientUsingScript CreateAsync(
           [NotNull] Guid ScriptId,
           [NotNull] Guid ClientId,
           bool IsActive
           )
        {
            return new ClientUsingScript(
                GuidGenerator.Create(),
                ScriptId,
                ClientId,
                IsActive,
                string.Empty
            );
        }
    }
}

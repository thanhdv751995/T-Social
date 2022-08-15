using Extention.Management.ScriptTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.ExpandScripts
{
    public class ExpandScriptManager : DomainService
    {
        public ExpandScript CreateAsync(
               [NotNull] Guid scripId,
               [NotNull] bool isActive,
               [NotNull] ScriptTypeEnum scriptType,
               string url
         )
        {
            return new ExpandScript(
                GuidGenerator.Create(),
                scripId,
                isActive,
                scriptType,
                url
            );
        }
    }
}

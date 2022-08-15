
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.ScriptDefaultTypes
{
    public class ScriptProfileTypeManager : DomainService
    {
        public ScriptDefaultType CreateAsync(
           [NotNull] Guid scriptId,
           [NotNull] Guid profileId
           )
        {
            return new ScriptDefaultType(
                GuidGenerator.Create(),
                scriptId,
                profileId
            );
        }
    }
}

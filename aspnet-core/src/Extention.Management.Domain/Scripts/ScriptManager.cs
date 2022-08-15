using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.Scripts
{
    public class ScriptManager : DomainService
    {
        public Script CreateAsync(
           [NotNull] string nameScript,
           [NotNull] bool isActive,
           [NotNull] bool isDefault,
           [NotNull] string value,
           [NotNull] Type.Type type
           )
        {
            return new Script(
                GuidGenerator.Create(),
                nameScript,
                isActive,
                isDefault,
                value,
                type
            );
        }
    }
}

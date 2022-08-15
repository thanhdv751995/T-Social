using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.ExtensionVariables
{
    public class ExtensionVariableManager : DomainService
    {
        public ExtensionVariable CreateAsync(
               string className,
               string value
         )
        {
            return new ExtensionVariable(
                GuidGenerator.Create(),
                className,
                value
            );
        }
    }
}

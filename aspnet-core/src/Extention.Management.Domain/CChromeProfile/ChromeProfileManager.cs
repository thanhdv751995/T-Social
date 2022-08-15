using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Extention.Management.CChromeProfile
{
    public class ChromeProfileManager : DomainService
    {
        public ChromeProfileManager()
        {
        }
        public ChromeProfile CreateAsync(
            string profile
           )
        {
            return new ChromeProfile(
                GuidGenerator.Create(),
                profile
            );
        }
    }
}

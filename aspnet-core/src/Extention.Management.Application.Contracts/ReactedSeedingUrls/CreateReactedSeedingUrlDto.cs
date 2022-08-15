using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ReactedSeedingUrls
{
    public class CreateReactedSeedingUrlDto
    {
        public Guid ClientId { get; set; }
        public string URL { get; set; }
    }
}

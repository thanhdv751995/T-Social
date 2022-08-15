using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.SeedingContent
{
    public class SeedingContentListDto
    {
        public Guid SeedingId { get; set; }
        public List<SeedingContentDto> SeedingContentDtos { get; set; }
    }
}

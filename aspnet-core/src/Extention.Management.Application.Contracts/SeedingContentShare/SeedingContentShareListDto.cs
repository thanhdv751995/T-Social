using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.SeedingContentShare
{
    public class SeedingContentShareListDto
    {
        public Guid SeedingId { get; set; }
        public List<SeedingContentShareDto> SeedingContentDtos { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.SeedingContentComment
{
    public class SeedingContentCommentListDto
    {
        public Guid SeedingId { get; set; }
        public List<SeedingContentCommentDto> SeedingContentDtos { get; set; }
    }
}

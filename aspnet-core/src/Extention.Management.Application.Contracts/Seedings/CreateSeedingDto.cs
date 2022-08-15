using Extention.Management.SeedingContent;
using Extention.Management.SeedingContentComment;
using Extention.Management.SeedingContentShare;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Seedings
{
    public class CreateSeedingDto
    {
        public string Name { get; set; }
        public int PostsWall { get; set; }
        public int PostsGroup { get; set; }
        public int Comments { get; set; }
        public int Reacts { get; set; }
        public int SharesWall { get; set; }
        public int SharesGroup { get; set; }
        public Guid GroupTypeId { get; set; }
        public List<SeedingContentDto> SeedingContentDtos { get; set; }
        public List<SeedingContentCommentDto> CommentContentList { get; set; }
        public List<SeedingContentShareDto> ShareContentList { get; set; }
        public string URL { get; set; }
    }
}

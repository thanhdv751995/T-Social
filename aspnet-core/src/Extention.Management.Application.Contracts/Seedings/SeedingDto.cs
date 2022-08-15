using Extention.Management.SeedingContent;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Seedings
{
    public class SeedingDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PostsWall { get; set; }
        public int PostsGroup { get; set; }
        public int Comments { get; set; }
        public int Reacts { get; set; }
        public string Processed { get; set; }
        public string CountReacts { get; set; }
        public string CountComments { get; set; }
        public string CountShareWall { get; set; }
        public string CountShareGroups { get; set; }
        public string CountPostWall { get; set; }
        public string CountPostGroups { get; set; }
        public int SharesWall { get; set; }
        public int SharesGroup { get; set; }
        public Guid GroupTypeId { get; set; }
        public string GroupName { get; set; }
        public bool IsFinish { get; set; }
        public string URL { get; set; }
        public List<SeedingContentDto> SeedingContentDtos { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.CommentedSeedingUrls
{
    public class CreateCommentedSeedingUrlDto
    {
        public Guid ClientId { get; set; }
        public string URL { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Commenteds
{
    public class CommentedDto
    {
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public string CommentContent { get; set; }
        public string URL { get; set; }
    }
}

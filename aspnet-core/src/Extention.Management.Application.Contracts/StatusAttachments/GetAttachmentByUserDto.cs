using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.StatusAttachments
{
    public class GetAttachmentByUserDto
    {
        public string UserName { get; set; }
        public Guid IdClient { get; set; }
        public List<string> URL { get; set; }
    }
}

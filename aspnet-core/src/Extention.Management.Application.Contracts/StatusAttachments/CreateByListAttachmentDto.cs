using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.StatusAttachments
{
    public class CreateByListAttachmentDto
    {
        public Guid IdUser { get; set; }
        public List<string> URL { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.StatusAttachments
{
    public class GetListAttachmentDto
    {
        public string clientName { get; set; }
        public Guid idClient { get; set; }
        public string URL { get; set; }
    }
}

using Extention.Management.EStatusType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.AttachmentsStore
{
    public class CreateAttachmentsStoreDto
    {
        public StatusType StatusType { get; set; }
        public string Url { get; set; }
    }
}

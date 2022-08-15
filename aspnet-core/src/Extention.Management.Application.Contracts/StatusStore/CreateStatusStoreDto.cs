using Extention.Management.EStatusType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.StatusStore
{
    public class CreateStatusStoreDto
    {
        public StatusType StatusType { get; set; }
        public string Content { get; set; }
    }
}

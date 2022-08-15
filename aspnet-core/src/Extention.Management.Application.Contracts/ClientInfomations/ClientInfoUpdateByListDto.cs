using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.ClientInfomations
{
    public class ClientInfoUpdateByListDto
    {
        public Guid IdUser { get; set; }
        public CreateUpdateClientInfomationDto createUpdateClientInfomationDtos { get; set; }
    }
}

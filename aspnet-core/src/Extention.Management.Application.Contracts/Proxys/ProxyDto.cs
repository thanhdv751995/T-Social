using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ExtensionsManagement.ProxyIpDtos
{
    public class ProxyDto : AuditedEntityDto<Guid>
    {
        public string ProxyIp { get; set; }
        public bool IsActive { get; set; }
        public int AccountOnProxy { get; set; }
    }
}

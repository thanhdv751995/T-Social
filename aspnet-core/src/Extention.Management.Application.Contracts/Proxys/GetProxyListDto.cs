using Extention.Management.EValue;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Proxy
{
    public class GetProxyListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}

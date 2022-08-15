using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ExtensionsManagement.ClientFacebooks
{
    public class GetClientListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}

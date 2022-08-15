using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.AccountActives
{
    public class GetAccountActiveDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}

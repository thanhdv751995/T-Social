using Extention.Management.EValue;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Scripts
{
    public class GetScriptListDto: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public string ID { get; set; }
        public Value Value { get; set; } = Value.All;
    }
}

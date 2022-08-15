using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Clients
{
    public class AccountFacebookDto
    {
        public int TaskSchedule { get; set; } = 0;
        public int TaskProcessing { get; set; } = 0;
        public int AccountOnline { get; set; } = 0;
        public int AccountCheckpoint { get; set; } = 0;
        public PagedResultDto<AccountDto> AccountDtos { get; set; }
    }
}

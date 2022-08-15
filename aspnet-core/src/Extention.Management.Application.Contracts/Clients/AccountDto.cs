using System;
using System.Collections.Generic;
using System.Text;

namespace Extention.Management.Clients
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string NameFacebook { get; set; }
        public string UserName { get; set; }
        public bool Online { get; set; }
        public string ProxyIp { get; set; }
        public bool IsActive { get; set; }
        public LastActivityDto LastActivity { get; set; }
        public int TotalTask { get; set; }
        public int TotalTaskFinish { get; set; }
        public int TotalTaskWait { get; set; }
        public CurrentTaskDto CurrentTask { get; set; }
        public bool HasCheckPoint { get; set; }
    }
}

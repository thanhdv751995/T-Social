using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensionsManagement.ClientFacebooks
{
    public class CreateUpdateClientDto
    {
        public string NameFacebook { get; set; }
        public string AvatarUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SecretKey { get; set; }
        public string Cookie { get; set; }
        public bool IsActive { get; set; }
        public string ProxyIp { get; set; }
        public string AccessToken { get; set; }
        public string ChromeProfile { get; set; }
        public bool Online { get; set; }
        public bool HasCheckPoint { get; set; }
        public string ComputerName { get; set; }
    }
}

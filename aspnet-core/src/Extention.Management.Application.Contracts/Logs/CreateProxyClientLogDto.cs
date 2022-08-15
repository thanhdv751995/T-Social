using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Logs
{
    public class CreateProxyClientLogDto
    {
        public string ProxyIP { get; set; }
        public string Body { get; set; }
        public string Method { get; set; }
    }
}

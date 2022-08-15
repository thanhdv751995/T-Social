using Extention.Management.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.Enum
{
    [Microsoft.AspNetCore.Mvc.Route("api/enum")]
    public class EnumController : ManagementController
    {
        public EnumController()
        {
        }
        //[HttpGet("Get-List-Profile-Type")]
        //public List<object> GetListProfileType()
        //{
        //    return _enumAppService.GetListProfileType();
        //}
        [HttpGet("Get-List-Type")]
        public List<object> GetListType()
        {
            return EnumAppService.GetListType();
        }
        [HttpGet("Get-List-Status-Type")]
        public List<object> GetListStatusType()
        {
            return EnumAppService.GetListStatusType();
        }
    }
}

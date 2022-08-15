using Extention.Management.VirtualMachines;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.VirtualMachine
{
    [Microsoft.AspNetCore.Mvc.Route("virtual-machine")]
    public class VirtualMachineController : ManagementController
    {
        private readonly VirtualMachineAppService _virtualMachineAppService;
        public VirtualMachineController(VirtualMachineAppService virtualMachineAppService)
        {
            _virtualMachineAppService = virtualMachineAppService;
        }

        [HttpPost("Create-Or-Update-Start-Chrome-Service")]
        public async Task CreateUpdateSartAsync([FromBody]CreateUpdateVirtualMachineDto createUpdateVirtualMachineDto)
        {
            await _virtualMachineAppService.CreateOrStartChromeServiceAsync(createUpdateVirtualMachineDto);
        }

        [HttpPost("Create-Or-Update-Close-Chrome-Service")]
        public async Task CreateUpdateCloseAsync([FromBody] CreateUpdateVirtualMachineDto createUpdateVirtualMachineDto)
        {
            await _virtualMachineAppService.CreateOrCloseChromeServiceAsync(createUpdateVirtualMachineDto);
        }

        [HttpPut("Active-VM")]
        public async Task UpdateActiveVMAsync([FromBody] CreateUpdateVirtualMachineDto createUpdateVirtualMachineDto)
        {
            await _virtualMachineAppService.UpdateAcitveVMAsync(createUpdateVirtualMachineDto);
        }

        [HttpGet("VM")]
        public string GetVM()
        {
            return _virtualMachineAppService.GetVM();
        }
        [HttpGet("list")]
        public async Task<List<VirtualMachineDto>> GetVirtualMachineAsync()
        {
            return await _virtualMachineAppService.GetVirtualMachineAsync();
        }
    }
}

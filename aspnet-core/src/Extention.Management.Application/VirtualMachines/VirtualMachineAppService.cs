using Extention.Management.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.VirtualMachines
{
    public class VirtualMachineAppService : ManagementAppService
    {
        private readonly IVirtualMachineRepository _virtualMachineRepository;
        private readonly VirtualMachineManager _virtualMachineManager;
        private readonly IClientRepository _clientRepository;
        public VirtualMachineAppService(IVirtualMachineRepository virtualMachineRepository,
            VirtualMachineManager virtualMachineManager,
            IClientRepository clientRepository)
        {
            _virtualMachineRepository = virtualMachineRepository;
            _virtualMachineManager = virtualMachineManager;
            _clientRepository = clientRepository;
        }

        public async Task CreateAsync(CreateUpdateVirtualMachineDto createUpdateVirtualMachineDto)
        {
            var VM = _virtualMachineManager.CreateAsync(createUpdateVirtualMachineDto.Name, true);

            await _virtualMachineRepository.InsertAsync(VM);
        }

        public async Task UpdateStartChromeServiceAsync(CreateUpdateVirtualMachineDto createUpdateVirtualMachineDto)
        {
            var VM = await _virtualMachineRepository.GetAsync(x => x.Name == createUpdateVirtualMachineDto.Name);

            VM.IsActive = true;

            await _virtualMachineRepository.UpdateAsync(VM);
        }

        public async Task UpdateCloseChromeServiceAsync(CreateUpdateVirtualMachineDto createUpdateVirtualMachineDto)
        {
            var VM = await _virtualMachineRepository.GetAsync(x => x.Name == createUpdateVirtualMachineDto.Name);

            VM.IsActive = false;

            await _virtualMachineRepository.UpdateAsync(VM);
        }

        public async Task CreateOrStartChromeServiceAsync(CreateUpdateVirtualMachineDto createUpdateVirtualMachineDto)
        {
            if(_virtualMachineRepository.Any(x => x.Name == createUpdateVirtualMachineDto.Name))
            {
                await UpdateStartChromeServiceAsync(createUpdateVirtualMachineDto);
            }
            else
            {
                await CreateAsync(createUpdateVirtualMachineDto);
            }
        }

        public async Task CreateOrCloseChromeServiceAsync(CreateUpdateVirtualMachineDto createUpdateVirtualMachineDto)
        {
            if (_virtualMachineRepository.Any(x => x.Name == createUpdateVirtualMachineDto.Name))
            {
                await UpdateCloseChromeServiceAsync(createUpdateVirtualMachineDto);
            }
            else
            {
                await CreateAsync(createUpdateVirtualMachineDto);
            }
        }

        public async Task UpdateAcitveVMAsync(CreateUpdateVirtualMachineDto createUpdateVirtualMachineDto)
        {
            var VM = await _virtualMachineRepository.GetAsync(x => x.Name == createUpdateVirtualMachineDto.Name);

            VM.IsActive = !VM.IsActive;

            await _virtualMachineRepository.UpdateAsync(VM);
        }

        public string GetVM()
        {
            string VMName = string.Empty;

            if(_virtualMachineRepository.Any(v => v.IsActive && _clientRepository.Any(c => c.ComputerName != v.Name)))
            {
                VMName = _virtualMachineRepository.FirstOrDefault(v => v.IsActive && _clientRepository.Any(c => c.ComputerName != v.Name)).Name;
            }
            else
            {
                VMName = _virtualMachineRepository.Where(x => x.IsActive).OrderBy(x => _clientRepository.Count(c => c.ComputerName == x.Name)).FirstOrDefault().Name;
            }
           
            return VMName;
        }

        public async Task<List<VirtualMachineDto>> GetVirtualMachineAsync()
        {
            var listVirtualMachine = await _virtualMachineRepository.GetListAsync();

            return ObjectMapper.Map<List<VirtualMachine>,List<VirtualMachineDto>>(listVirtualMachine);
        }
    }
}

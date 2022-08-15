import { Component, OnInit } from '@angular/core';
import { VirtualMachineService } from 'src/serivce/Virtual-Machine/virtualMachine.service';

@Component({
  selector: 'app-virtual-machine',
  templateUrl: './virtual-machine.component.html',
  styleUrls: ['./virtual-machine.component.css']
})
export class VirtualMachineComponent implements OnInit {
  nullLastModified = '-';
  listVirtualMachine: any;
  constructor(private virtualMachineService: VirtualMachineService) { }

  ngOnInit() {
    this.getListVirtualMachine();
  }
  getListVirtualMachine() {
    this.virtualMachineService.getListVirtualMachine().subscribe(data => {
      if (data) {
        this.listVirtualMachine = data;
      }
    })
  }

  updateVirtualMachine(virtualMachineName: any) {
    this.virtualMachineService.updateActive({ name: virtualMachineName }).subscribe(() => {
      this.getListVirtualMachine();
    })
  }
}

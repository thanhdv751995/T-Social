/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { VirtualMachineService } from './virtualMachine.service';

describe('Service: VirtualMachine', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [VirtualMachineService]
    });
  });

  it('should ...', inject([VirtualMachineService], (service: VirtualMachineService) => {
    expect(service).toBeTruthy();
  }));
});

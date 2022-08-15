/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SignalRService as SignalRService } from './signalR.service';

describe('Service: SignalRService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SignalRService]
    });
  });

  it('should ...', inject([SignalRService], (service: SignalRService) => {
    expect(service).toBeTruthy();
  }));
});

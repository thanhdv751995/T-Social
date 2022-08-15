/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ProxyClientLogsServiceService } from './proxyClientLogsService.service';

describe('Service: ProxyClientLogsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProxyClientLogsServiceService]
    });
  });

  it('should ...', inject([ProxyClientLogsServiceService], (service: ProxyClientLogsServiceService) => {
    expect(service).toBeTruthy();
  }));
});

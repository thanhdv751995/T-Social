import { TestBed } from '@angular/core/testing';

import { ClientInfomationService } from './client-infomation.service';

describe('ClientInfomationService', () => {
  let service: ClientInfomationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClientInfomationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

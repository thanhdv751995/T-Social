import { TestBed } from '@angular/core/testing';

import { ClientActiveService } from './client-active.service';

describe('ClientActiveService', () => {
  let service: ClientActiveService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClientActiveService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

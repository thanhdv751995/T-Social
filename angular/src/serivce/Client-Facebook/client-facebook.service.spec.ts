import { TestBed } from '@angular/core/testing';

import { ClientFacebookService } from './client-facebook.service';

describe('ClientFacebookService', () => {
  let service: ClientFacebookService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClientFacebookService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

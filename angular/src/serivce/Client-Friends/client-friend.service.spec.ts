import { TestBed } from '@angular/core/testing';

import { ClientFriendService } from './client-friend.service';

describe('ClientFriendService', () => {
  let service: ClientFriendService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClientFriendService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

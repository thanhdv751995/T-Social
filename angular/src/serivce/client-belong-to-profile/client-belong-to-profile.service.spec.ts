/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ClientBelongToProfileService } from './client-belong-to-profile.service';

describe('Service: ClientBelongToProfile', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ClientBelongToProfileService]
    });
  });

  it('should ...', inject([ClientBelongToProfileService], (service: ClientBelongToProfileService) => {
    expect(service).toBeTruthy();
  }));
});

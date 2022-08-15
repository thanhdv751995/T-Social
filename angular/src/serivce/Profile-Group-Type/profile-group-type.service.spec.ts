import { TestBed } from '@angular/core/testing';

import { ProfileGroupTypeService } from './profile-group-type.service';

describe('ProfileGroupTypeService', () => {
  let service: ProfileGroupTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProfileGroupTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

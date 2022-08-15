/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ScriptDefaultProfileService } from './script-default-profile.service';

describe('Service: ScriptDefaultProfile', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ScriptDefaultProfileService]
    });
  });

  it('should ...', inject([ScriptDefaultProfileService], (service: ScriptDefaultProfileService) => {
    expect(service).toBeTruthy();
  }));
});

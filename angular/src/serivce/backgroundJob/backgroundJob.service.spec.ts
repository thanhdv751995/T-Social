/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { BackgroundJobService } from './backgroundJob.service';

describe('Service: BackgroundJob', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BackgroundJobService]
    });
  });

  it('should ...', inject([BackgroundJobService], (service: BackgroundJobService) => {
    expect(service).toBeTruthy();
  }));
});

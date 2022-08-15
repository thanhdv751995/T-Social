/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { StatusTypeService } from './status-type.service';

describe('Service: StatusType', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [StatusTypeService]
    });
  });

  it('should ...', inject([StatusTypeService], (service: StatusTypeService) => {
    expect(service).toBeTruthy();
  }));
});

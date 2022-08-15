/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ChromeService } from './chrome.service';

describe('Service: Chrome', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ChromeService]
    });
  });

  it('should ...', inject([ChromeService], (service: ChromeService) => {
    expect(service).toBeTruthy();
  }));
});

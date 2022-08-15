/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ProxyUsingScriptService } from './proxy-using-script.service';

describe('Service: ProxyUsingScript', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProxyUsingScriptService]
    });
  });

  it('should ...', inject([ProxyUsingScriptService], (service: ProxyUsingScriptService) => {
    expect(service).toBeTruthy();
  }));
});

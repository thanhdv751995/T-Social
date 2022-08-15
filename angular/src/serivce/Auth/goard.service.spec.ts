import { TestBed } from '@angular/core/testing';
import { AuthGuard } from './goard.service';


describe('GoardService', () => {
  let service: AuthGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthGuard);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});



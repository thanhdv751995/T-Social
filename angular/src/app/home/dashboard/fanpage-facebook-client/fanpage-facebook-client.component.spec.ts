import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FanpageFacebookClientComponent } from './fanpage-facebook-client.component';

describe('FanpageFacebookClientComponent', () => {
  let component: FanpageFacebookClientComponent;
  let fixture: ComponentFixture<FanpageFacebookClientComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FanpageFacebookClientComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FanpageFacebookClientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

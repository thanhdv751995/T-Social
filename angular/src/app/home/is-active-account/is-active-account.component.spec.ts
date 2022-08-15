import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IsActiveAccountComponent } from './is-active-account.component';

describe('IsActiveAccountComponent', () => {
  let component: IsActiveAccountComponent;
  let fixture: ComponentFixture<IsActiveAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IsActiveAccountComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IsActiveAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

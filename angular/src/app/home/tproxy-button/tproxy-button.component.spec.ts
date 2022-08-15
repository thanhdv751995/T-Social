import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TProxyButtonComponent } from './tproxy-button.component';

describe('TProxyButtonComponent', () => {
  let component: TProxyButtonComponent;
  let fixture: ComponentFixture<TProxyButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TProxyButtonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TProxyButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

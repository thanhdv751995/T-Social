import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientFacebookComponent } from './client-facebook.component';

describe('ClientFacebookComponent', () => {
  let component: ClientFacebookComponent;
  let fixture: ComponentFixture<ClientFacebookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClientFacebookComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientFacebookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

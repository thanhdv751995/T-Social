import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupFacebookClientComponent } from './group-facebook-client.component';

describe('GroupFacebookClientComponent', () => {
  let component: GroupFacebookClientComponent;
  let fixture: ComponentFixture<GroupFacebookClientComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GroupFacebookClientComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupFacebookClientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListStatusInGroupComponent } from './list-status-in-group.component';

describe('ListStatusInGroupComponent', () => {
  let component: ListStatusInGroupComponent;
  let fixture: ComponentFixture<ListStatusInGroupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListStatusInGroupComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListStatusInGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

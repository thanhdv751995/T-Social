import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientActivityLogsComponent } from './client-activity-logs.component';

describe('ClientActivityLogsComponent', () => {
  let component: ClientActivityLogsComponent;
  let fixture: ComponentFixture<ClientActivityLogsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClientActivityLogsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientActivityLogsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

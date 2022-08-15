import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientSeedingDashboardComponent } from './client-seeding-dashboard.component';

describe('ClientSeedingDashboardComponent', () => {
  let component: ClientSeedingDashboardComponent;
  let fixture: ComponentFixture<ClientSeedingDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClientSeedingDashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientSeedingDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

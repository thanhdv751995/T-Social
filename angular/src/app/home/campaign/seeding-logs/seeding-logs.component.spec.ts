import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeedingLogsComponent } from './seeding-logs.component';

describe('SeedingLogsComponent', () => {
  let component: SeedingLogsComponent;
  let fixture: ComponentFixture<SeedingLogsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SeedingLogsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SeedingLogsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

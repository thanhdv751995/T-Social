import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientActivityChartComponent } from './client-activity-chart.component';

describe('ClientActivityChartComponent', () => {
  let component: ClientActivityChartComponent;
  let fixture: ComponentFixture<ClientActivityChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClientActivityChartComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientActivityChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { ActiviteChartService } from './activite-chart.service';

describe('ActiviteChartService', () => {
  let service: ActiviteChartService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ActiviteChartService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

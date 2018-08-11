import { TestBed, inject } from '@angular/core/testing';

import { TotalsService } from './totals.service';

describe('TotalsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TotalsService]
    });
  });

  it('should be created', inject([TotalsService], (service: TotalsService) => {
    expect(service).toBeTruthy();
  }));
});

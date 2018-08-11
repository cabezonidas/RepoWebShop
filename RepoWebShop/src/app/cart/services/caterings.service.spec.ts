import { TestBed, inject } from '@angular/core/testing';

import { CateringsService } from './caterings.service';

describe('CateringsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CateringsService]
    });
  });

  it('should be created', inject([CateringsService], (service: CateringsService) => {
    expect(service).toBeTruthy();
  }));
});

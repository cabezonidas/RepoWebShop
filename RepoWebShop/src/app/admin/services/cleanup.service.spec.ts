import { TestBed, inject } from '@angular/core/testing';

import { CleanupService } from './cleanup.service';

describe('CleanupService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CleanupService]
    });
  });

  it('should be created', inject([CleanupService], (service: CleanupService) => {
    expect(service).toBeTruthy();
  }));
});

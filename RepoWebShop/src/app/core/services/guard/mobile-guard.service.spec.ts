import { TestBed, inject } from '@angular/core/testing';

import { MobileGuardService } from './mobile-guard.service';

describe('MobileGuardService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MobileGuardService]
    });
  });

  it('should be created', inject([MobileGuardService], (service: MobileGuardService) => {
    expect(service).toBeTruthy();
  }));
});

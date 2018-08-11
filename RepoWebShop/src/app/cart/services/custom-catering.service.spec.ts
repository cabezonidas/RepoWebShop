import { TestBed, inject } from '@angular/core/testing';

import { CustomCateringService } from './custom-catering.service';

describe('CustomCateringService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CustomCateringService]
    });
  });

  it('should be created', inject([CustomCateringService], (service: CustomCateringService) => {
    expect(service).toBeTruthy();
  }));
});

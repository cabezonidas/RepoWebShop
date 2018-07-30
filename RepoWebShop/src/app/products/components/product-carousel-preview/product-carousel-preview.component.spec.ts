import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCarouselPreviewComponent } from './product-carousel-preview.component';

describe('ProductCarouselPreviewComponent', () => {
  let component: ProductCarouselPreviewComponent;
  let fixture: ComponentFixture<ProductCarouselPreviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductCarouselPreviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductCarouselPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

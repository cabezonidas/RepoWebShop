import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductSubtotalComponent } from './product-subtotal.component';

describe('ProductSubtotalComponent', () => {
  let component: ProductSubtotalComponent;
  let fixture: ComponentFixture<ProductSubtotalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductSubtotalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductSubtotalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

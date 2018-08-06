import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewCateringSubtotalHeaderComponent } from './new-catering-subtotal-header.component';

describe('NewCateringSubtotalHeaderComponent', () => {
  let component: NewCateringSubtotalHeaderComponent;
  let fixture: ComponentFixture<NewCateringSubtotalHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewCateringSubtotalHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewCateringSubtotalHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

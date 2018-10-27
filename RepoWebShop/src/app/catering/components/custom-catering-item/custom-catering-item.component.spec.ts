import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomCateringItemComponent } from './custom-catering-item.component';

describe('CustomCateringItemComponent', () => {
  let component: CustomCateringItemComponent;
  let fixture: ComponentFixture<CustomCateringItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomCateringItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomCateringItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

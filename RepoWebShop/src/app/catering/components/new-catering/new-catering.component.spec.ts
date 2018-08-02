import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewCateringComponent } from './new-catering.component';

describe('NewCateringComponent', () => {
  let component: NewCateringComponent;
  let fixture: ComponentFixture<NewCateringComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewCateringComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewCateringComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

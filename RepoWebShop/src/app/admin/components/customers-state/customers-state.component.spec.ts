import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomersStateComponent } from './customers-state.component';

describe('CustomersStateComponent', () => {
  let component: CustomersStateComponent;
  let fixture: ComponentFixture<CustomersStateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomersStateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomersStateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

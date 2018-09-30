import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomersShellComponent } from './customers-shell.component';

describe('CustomersShellComponent', () => {
  let component: CustomersShellComponent;
  let fixture: ComponentFixture<CustomersShellComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomersShellComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomersShellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

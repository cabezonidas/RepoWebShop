import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllOrdersShellComponent } from './all-orders-shell.component';

describe('AllOrdersShellComponent', () => {
  let component: AllOrdersShellComponent;
  let fixture: ComponentFixture<AllOrdersShellComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllOrdersShellComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllOrdersShellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

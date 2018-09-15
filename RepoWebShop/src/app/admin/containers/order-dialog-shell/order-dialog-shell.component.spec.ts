import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderDialogShellComponent } from './order-dialog-shell.component';

describe('OrderDialogShellComponent', () => {
  let component: OrderDialogShellComponent;
  let fixture: ComponentFixture<OrderDialogShellComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrderDialogShellComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderDialogShellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ActiveOrdersShellComponent } from './active-orders-shell.component';

describe('ActiveOrdersShellComponent', () => {
  let component: ActiveOrdersShellComponent;
  let fixture: ComponentFixture<ActiveOrdersShellComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ActiveOrdersShellComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ActiveOrdersShellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

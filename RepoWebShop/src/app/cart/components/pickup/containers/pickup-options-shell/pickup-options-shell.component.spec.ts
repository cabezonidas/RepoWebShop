import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PickupOptionsShellComponent } from './pickup-options-shell.component';

describe('PickupOptionsShellComponent', () => {
  let component: PickupOptionsShellComponent;
  let fixture: ComponentFixture<PickupOptionsShellComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PickupOptionsShellComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PickupOptionsShellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

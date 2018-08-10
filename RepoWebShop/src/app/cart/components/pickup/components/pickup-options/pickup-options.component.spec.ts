import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PickupOptionsComponent } from './pickup-options.component';

describe('PickupOptionsComponent', () => {
  let component: PickupOptionsComponent;
  let fixture: ComponentFixture<PickupOptionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PickupOptionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PickupOptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

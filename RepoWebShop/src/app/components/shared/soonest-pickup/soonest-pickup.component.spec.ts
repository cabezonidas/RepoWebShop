import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SoonestPickupComponent } from './soonest-pickup.component';

describe('SoonestPickupComponent', () => {
  let component: SoonestPickupComponent;
  let fixture: ComponentFixture<SoonestPickupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SoonestPickupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SoonestPickupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

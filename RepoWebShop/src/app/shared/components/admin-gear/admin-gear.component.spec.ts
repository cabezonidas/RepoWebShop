import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminGearComponent } from './admin-gear.component';

describe('AdminGearComponent', () => {
  let component: AdminGearComponent;
  let fixture: ComponentFixture<AdminGearComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminGearComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminGearComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

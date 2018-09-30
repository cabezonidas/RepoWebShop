import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MobileActivationCodeComponent } from './mobile-activation-code.component';

describe('MobileActivationCodeComponent', () => {
  let component: MobileActivationCodeComponent;
  let fixture: ComponentFixture<MobileActivationCodeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MobileActivationCodeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MobileActivationCodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

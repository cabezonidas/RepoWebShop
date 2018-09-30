import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailActivationCodeComponent } from './email-activation-code.component';

describe('EmailActivationCodeComponent', () => {
  let component: EmailActivationCodeComponent;
  let fixture: ComponentFixture<EmailActivationCodeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmailActivationCodeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmailActivationCodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

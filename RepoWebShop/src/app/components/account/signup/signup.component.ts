import { Component, OnInit, HostBinding } from '@angular/core';
// import { AngularFire, AuthProviders, AuthMethods } from 'angularfire2';
import { Router } from '@angular/router';
import { moveIn, fallIn } from '../../../animations/router.animations';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '../../../../../node_modules/@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { EmailRegistration } from '../../../classes/EmailRegistration';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
  animations: [moveIn(), fallIn()]
})
export class SignupComponent implements OnInit {

  state = '';
  error: any;
  hide = true;

  emailGroup: FormGroup;
  confirmEmailGroup: FormGroup;

  emailGroupEditable = true;
  emailTaken = false;
  emailCodeSubmitted = false;
  emailCodeSent = false;

  constructor(private _formBuilder: FormBuilder, private auth: AuthService, private router: Router) {}

  @HostBinding('@moveIn') role = '';
  returnUrl = '';

  ngOnInit() {
    this.auth.returnUrl.subscribe(url => this.returnUrl = url);
    this.auth.user.subscribe(user$ => {
      if (user$) {
        this.router.navigate([ this.returnUrl ? this.returnUrl : '/members' ]);
      }
    });

    this.emailGroup = this._formBuilder.group({
      firstName: ['', Validators.compose([Validators.required, Validators.maxLength(50)])],
      lastName: ['', Validators.compose([Validators.required, Validators.maxLength(50)])],
      email: ['', Validators.compose([Validators.required, Validators.email]), this.emailNotTaken.bind(this)],
      password: ['', Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(50)])]
    });
    this.confirmEmailGroup = this._formBuilder.group({
      emailCode: ['',
        Validators.compose([Validators.required, Validators.minLength(4), Validators.maxLength(4)]),
        this.matchingValidationCode.bind(this)]
    });
  }

  matchingValidationCode(control: AbstractControl): {[key: string]: any} | null {
    return this.auth.registerUserEmail(this.current(), control.value)
    .map(res => {
      if (res) {
        this.auth.loadUser();
      }
      return { emailCode: false };
    });
  }

  emailNotTaken(control: AbstractControl): {[key: string]: any} | null {
    return this.auth.isEmailAvailable(control.value)
    .map(res => {
      this.emailTaken = res !== true;
      return res === true ? null : { emailTaken: true };
    });
  }

  bookEmail = () => {
    this.emailGroupEditable = false;
    this.emailCodeSubmitted = true;
    this.auth.bookEmail(this.current()).subscribe(() => {
      this.emailCodeSent = true;
    }, () => {
      this.emailCodeSubmitted = false;
      this.emailCodeSent = false;
    });
  }

  current = (): EmailRegistration => new EmailRegistration(this.emailGroup.value.firstName,
      this.emailGroup.value.lastName, this.emailGroup.value.email, this.emailGroup.value.password)

}

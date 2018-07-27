import { Component, OnInit, HostBinding, OnDestroy } from '@angular/core';
// import { AngularFire, AuthProviders, AuthMethods } from 'angularfire2';
import { Router } from '@angular/router';
import { moveIn, fallIn } from '../../../animations/router.animations';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '../../../../../node_modules/@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { EmailRegistration } from '../../../classes/EmailRegistration';
import { Subscription, Observable } from '../../../../../node_modules/rxjs';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
  animations: [moveIn(), fallIn()]
})
export class SignupComponent implements OnInit, OnDestroy {

  state = '';
  error: any;
  hide = true;

  emailGroup: FormGroup;
  confirmEmailGroup: FormGroup;

  emailGroupEditable = true;
  emailTaken = false;
  emailCodeSubmitted = false;
  emailCodeSent = false;

  emailCodeSent$ = new Subscription();
  returnUrl$ = new Subscription();
  user$ = new Subscription();

  constructor(private _formBuilder: FormBuilder, private auth: AuthService, private router: Router) {}

  @HostBinding('@moveIn') role = '';
  returnUrl = '';

  ngOnInit() {
    this.returnUrl$ = this.auth.returnUrl.subscribe(url => this.returnUrl = url);
    this.user$ = this.auth.user.subscribe(user$ => {
      if (user$) {
        this.auth.returnToUrl(this.returnUrl);
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
    .map(appUser => {
      if (appUser) {
        this.auth.userSource.next(appUser);
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
    this.emailCodeSent$.unsubscribe();
    this.emailGroupEditable = false;
    this.emailCodeSubmitted = true;
    this.emailCodeSent$ = this.auth.bookEmail(this.current()).subscribe(() => this.emailCodeSent = true) ;
  }

  current = (): EmailRegistration => new EmailRegistration(this.emailGroup.value.firstName,
      this.emailGroup.value.lastName, this.emailGroup.value.email, this.emailGroup.value.password)

  ngOnDestroy() {
    this.returnUrl$.unsubscribe();
    this.user$.unsubscribe();
    this.emailCodeSent$.unsubscribe();
  }

}

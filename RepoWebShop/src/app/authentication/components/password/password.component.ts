import { Component, OnInit, HostBinding, OnDestroy, ViewChild } from '@angular/core';
import { moveIn, moveInLeft } from '../../../animations/router.animations';
import { AuthService } from '../../services/auth.service';
import { IAppUser } from '../../interfaces/iapp-user';
import { Subscription } from 'rxjs';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { MatStepper } from '@angular/material';
import { AppService } from '../../../core/services/app/app.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-password',
  templateUrl: './password.component.html',
  styleUrls: ['./password.component.scss'],
  animations: [moveIn(), moveInLeft()]
})
export class PasswordComponent implements OnInit, OnDestroy {

  returnUrl = '';
  state = '';
  hide = true;
  user: IAppUser = null;

  emailGroup: FormGroup;
  confirmEmailGroup: FormGroup;
  newPasswordGroup: FormGroup;

  emailCodeSubmitted = false;
  emailCodeSent = false;
  emailNotFound = false;

  userSub = new Subscription();
  stepper$ = new Subscription();
  emailCodeSent$ = new Subscription();
  returnUrl$ = new Subscription();
  savePassword$ = new Subscription();
  constructor(private _formBuilder: FormBuilder, private auth: AuthService, private appService: AppService) { }
  @HostBinding('@moveIn') role = '';
  @ViewChild('stepper') stepper: MatStepper;

  ngOnInit() {
    this.returnUrl$ = this.appService.returnUrl.subscribe(url => this.returnUrl = url);
    this.stepper$ = this.stepper.selectionChange.subscribe(() => this.sendCodeToEmail());
    this.emailGroup = this._formBuilder.group({
      email: ['', Validators.compose([Validators.required, Validators.email]), this.emailFound.bind(this)],
    });
    this.confirmEmailGroup = this._formBuilder.group({
      emailCode: ['',
        Validators.compose([Validators.required, Validators.minLength(4), Validators.maxLength(4)]),
        this.matchingValidationCode.bind(this)]
    });
    this.newPasswordGroup = this._formBuilder.group({
      newPass: ['', Validators.compose([Validators.required, Validators.minLength(6)])]});
    this.userSub = this.appService.user.subscribe(appUser => {
      this.user = appUser;
    });
  }

  sendCodeToEmail = () => {
    this.emailCodeSent$.unsubscribe();
    this.emailCodeSubmitted = true;
    this.emailCodeSent$ = this.auth.recoverEmail(this.emailGroup.value.email).subscribe(() => this.emailCodeSent = true) ;
  }

  emailFound(control: AbstractControl): {[key: string]: any} | null {
    return this.auth.isEmailAvailable(control.value).pipe(
    map(res => {
      this.emailNotFound = res !== false;
      return res === false ? null : { emailNotFound: true };
    }));
  }

  matchingValidationCode(control: AbstractControl): {[key: string]: any} | null {
    return this.auth.activateRecoveredEmail(this.emailGroup.value.email, control.value).pipe(
    map(appUser => {
      if (appUser) {
        this.appService.setUser(appUser);
      }
      return { emailCode: false };
    }));
  }

  savePassword() {
    this.savePassword$.unsubscribe();
    this.savePassword$ = this.auth.updatePassword(this.newPasswordGroup.value.newPass)
      .subscribe(() => this.appService.returnToUrl(this.returnUrl));
  }

  ngOnDestroy() {
    this.userSub.unsubscribe();
    this.stepper$.unsubscribe();
    this.emailCodeSent$.unsubscribe();
    this.returnUrl$.unsubscribe();
    this.savePassword$.unsubscribe();
  }

}

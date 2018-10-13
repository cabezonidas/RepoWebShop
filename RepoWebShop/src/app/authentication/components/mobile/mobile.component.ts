import { Component, OnInit, HostBinding, ViewChild, OnDestroy } from '@angular/core';
import { moveIn, fallIn, moveInLeft } from '../../../animations/router.animations';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { IAppUser } from '../../interfaces/iapp-user';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { SmsService } from '../../services/sms.service';
import { Subscription } from 'rxjs';
import { AppService } from '../../../core/services/app/app.service';
import { map } from 'rxjs/operators';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-mobile',
  templateUrl: './mobile.component.html',
  styleUrls: ['./mobile.component.scss'],
  animations: [moveIn(), fallIn(), moveInLeft()]
})
export class MobileComponent implements OnInit, OnDestroy {

  user: IAppUser;
  state = '';
  returnUrl = '';
  mobileForm: FormGroup;
  codeForm: FormGroup;
  codeSubmitted = false;
  codeSent = false;
  returnUrl$ = new Subscription();
  user$ = new Subscription();
  codeSent$ = new Subscription();

  constructor(private router: Router, private auth: AuthService, private fb: FormBuilder,
    private sms: SmsService, private app: AppService, private titleService: Title) {

      this.mobileForm = fb.group({
        'mobile': [null, Validators.required, this.validMobile.bind(this)],
        'country': ['54', Validators.required]
      });
      this.codeForm = fb.group({
        'code': [null, Validators.required, this.validCode.bind(this)]
      });

  }

  @HostBinding('@moveIn') role = '';

  ngOnInit() {
    this.titleService.setTitle('Celular');
    this.returnUrl$ = this.app.returnUrl.subscribe(url => this.returnUrl = url);
    this.user$ = this.app.user.subscribe(user$ => {
      this.user = user$;
    });
    // this.user$ = this.auth.loadUser().subscribe(user => {
    //   this.user = user;
    // });
  }

  validCode(control: AbstractControl): {[key: string]: any} | null {
    return this.auth.confirmMobile(control.value).pipe(
      map(res => {
        if (res) {
          this.app.returnToUrl(this.returnUrl);
        }
        return res === true ? null : { emailTaken: true };
    }));
  }

  validMobile(control: AbstractControl): {[key: string]: any} | null {
    return this.sms.isValidMobile(this.mobileForm.value.country + control.value).pipe(
    map(res => {
      return res === true ? null : { emailTaken: true };
    }));
  }

  sendCodeToNumber = () => {
    this.codeSubmitted = true;
    this.codeSent$.unsubscribe();
    this.codeSent$ = this.auth.registerMobile(this.mobileForm.value.country + this.mobileForm.value.mobile)
      .subscribe(() => this.codeSent = true);
  }

  ngOnDestroy() {
    this.returnUrl$.unsubscribe();
    this.user$.unsubscribe();
    this.codeSent$.unsubscribe();
  }

}

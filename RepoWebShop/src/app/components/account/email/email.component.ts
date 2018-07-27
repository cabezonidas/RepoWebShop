import { Component, OnInit, HostBinding, OnDestroy, HostListener } from '@angular/core';
import { debounceTime, throttleTime  } from 'rxjs/operators';
import { Router } from '@angular/router';
import { fromEvent, Observable } from 'rxjs';
import { moveIn, fallIn } from '../../../animations/router.animations';
import { AuthService } from '../../../services/auth.service';
import { EmailLogin } from '../../../classes/EmailLogin';
import { FormBuilder, FormGroup, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
import { HttpErrorResponse } from '../../../../../node_modules/@angular/common/http';
import { Subscription, combineLatest } from '../../../../../node_modules/rxjs';
@Component({
  selector: 'app-email',
  templateUrl: './email.component.html',
  styleUrls: ['./email.component.scss'],
  animations: [moveIn(), fallIn()]
})
export class EmailComponent implements OnInit, OnDestroy {

  state: '';
  invalidCredentials = '';
  rForm: FormGroup;
  hide = true;
  returnUrl = '';

  returnUrlSub = new Subscription();
  userSub = new Subscription();
  onSubmitSub = new Subscription();

  constructor(private router: Router, private auth: AuthService, private fb: FormBuilder) {
    this.rForm = fb.group({
      'email': [null, Validators.compose([Validators.required, Validators.email])],
      'password': [null, Validators.compose([Validators.required, Validators.minLength(6)])]
    });
  }

  @HostBinding('@moveIn') role = '';

  ngOnInit() {
    this.returnUrlSub = this.auth.returnUrl.subscribe(url => this.returnUrl = url);
    this.userSub = this.auth.user.subscribe(user$ => {
      if (user$) {
        this.auth.returnToUrl(this.returnUrl);
      }
    });
  }

  onSubmit = () => {
    const emailLogon = new EmailLogin(this.rForm.value.email, this.rForm.value.password);
    this.onSubmitSub.unsubscribe();
    this.onSubmitSub = this.auth.emailLogin(emailLogon)
      .subscribe(appUser => {
        if (appUser) {
          this.auth.userSource.next(appUser);
        } else {
          this.invalidCredentials = 'Email o contraseña incorrecta';
        }
      });
    }

  ngOnDestroy(): void {
    this.returnUrlSub.unsubscribe();
    this.userSub.unsubscribe();
    this.onSubmitSub.unsubscribe();
  }
}

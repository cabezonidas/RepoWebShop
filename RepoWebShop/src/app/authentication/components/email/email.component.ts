import { Component, OnInit, HostBinding, OnDestroy, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { moveIn, fallIn } from '../../../animations/router.animations';
import { AuthService } from '../../services/auth.service';
import { FormBuilder, FormGroup, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
import { Subscription } from 'rxjs';
import { EmailLogin } from '../../classes/EmailLogin';
import { AppService } from '../../../core/services/app/app.service';
import { Title } from '@angular/platform-browser';
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

  constructor(private titleService: Title, private appService: AppService, private auth: AuthService, private fb: FormBuilder) {
    this.rForm = fb.group({
      'email': [null, Validators.compose([Validators.required, Validators.email])],
      'password': [null, Validators.compose([Validators.required, Validators.minLength(6)])]
    });
  }

  @HostBinding('@moveIn') role = '';

  ngOnInit() {
    this.titleService.setTitle('Email');
    this.returnUrlSub = this.appService.returnUrl.subscribe(url => this.returnUrl = url);
    this.userSub = this.appService.user.subscribe(user$ => {
      if (user$) {
        this.appService.returnToUrl(this.returnUrl);
      }
    });
  }

  onSubmit = () => {
    const emailLogon = new EmailLogin(this.rForm.value.email, this.rForm.value.password);
    this.onSubmitSub.unsubscribe();
    this.onSubmitSub = this.auth.emailLogin(emailLogon)
      .subscribe(appUser => {
        if (appUser) {
          this.appService.setUser(appUser);
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

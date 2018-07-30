import { Component, OnInit, HostBinding, OnDestroy } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import { Router } from '@angular/router';
import { moveIn } from '../../../animations/router.animations';
import * as firebase from 'firebase';
import { AuthService } from '../../services/auth.service';
import { Subscription } from 'rxjs';
import { AppService } from '../../../core/services/app/app.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  animations: [moveIn()]
})
export class LoginComponent implements OnInit, OnDestroy {

  error: any;
  returnUrl = '';
  afUser: firebase.User;
  backEndLogin$ = new Subscription();
  loginSuccess$ = new Subscription();
  returnUrl$ = new Subscription();
  userSub$ = new Subscription();

  constructor(public afAuth: AngularFireAuth, private router: Router, private auth: AuthService, private appService: AppService) { }

  @HostBinding('@moveIn') role = '';

  loginFb() {
    this.afAuth.auth.signInWithPopup(new firebase.auth.FacebookAuthProvider())
      .then(this.loginSuccess)
      .catch(this.loginError);
  }

  loginGoogle() {
    this.afAuth.auth.signInWithPopup(new firebase.auth.GoogleAuthProvider())
      .then(this.loginSuccess)
      .catch(this.loginError);
  }

  loginSuccess = () => {
    this.loginSuccess$.unsubscribe();
    this.backEndLogin$.unsubscribe();

    this.loginSuccess$ = this.afAuth.user.subscribe(user$ => {
      this.backEndLogin$ = this.auth.socialLogin(user$).subscribe(appUser => this.appService.setUser(appUser));
    }, this.loginError);
  }

  loginError = (err) => {
    this.error = 'No se pudo iniciar sesión. Intenta con otro medio.';
    console.warn(err);
  }

  ngOnInit() {
    this.returnUrl$ = this.appService.returnUrl.subscribe(url => this.returnUrl = url);
    this.userSub$ = this.appService.user.subscribe(user$ => {
      if (user$) {
        this.appService.returnToUrl(this.returnUrl);
      }
    });
  }

  ngOnDestroy() {
    this.loginSuccess$.unsubscribe();
    this.backEndLogin$.unsubscribe();
    this.returnUrl$.unsubscribe();
    this.userSub$.unsubscribe();
  }

}

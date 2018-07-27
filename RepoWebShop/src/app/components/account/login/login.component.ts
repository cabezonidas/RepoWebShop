import { Component, OnInit, HostBinding, OnDestroy } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import { Router } from '@angular/router';
import { moveIn } from '../../../animations/router.animations';
import * as firebase from 'firebase';
import { AuthService } from '../../../services/auth.service';
import { Subscription } from '../../../../../node_modules/rxjs';

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

  constructor(public afAuth: AngularFireAuth, private router: Router, private auth: AuthService) { }

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
      this.backEndLogin$ = this.auth.socialLogin(user$).subscribe(appUser => this.auth.userSource.next(appUser));
    }, this.loginError);
  }

  loginError = (err) => {
    this.error = 'No se pudo iniciar sesión. Intenta con otro medio.';
    console.warn(err);
  }

  ngOnInit() {
    this.returnUrl$ = this.auth.returnUrl.subscribe(url => this.returnUrl = url);
    this.userSub$ = this.auth.user.subscribe(user$ => {
      if (user$) {
        this.auth.returnToUrl(this.returnUrl);
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

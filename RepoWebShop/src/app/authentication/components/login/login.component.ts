import { Component, OnInit, HostBinding, OnDestroy } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import { Router } from '@angular/router';
import { moveIn } from '../../../animations/router.animations';
// import * as firebase from 'firebase/auth';
import * as firebase from 'firebase/app';
import 'firebase/auth';
import { AuthService } from '../../services/auth.service';
import { Subscription } from 'rxjs';
import { AppService } from '../../../core/services/app/app.service';
import { Title } from '@angular/platform-browser';
import { switchMap, tap, filter } from 'rxjs/operators';

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
  returnUrl$ = new Subscription();
  firebaseUser: firebase.User;

  constructor(public afAuth: AngularFireAuth, private titleService: Title,
    private router: Router, private auth: AuthService, private app: AppService) { }

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

  }

  loginError = (err) => {
    this.error = 'No se pudo iniciar sesión. Intenta con otro medio.';
    console.warn(err);
  }

  ngOnInit() {
    this.titleService.setTitle('Inicio de sesión');
    this.returnUrl$ = this.app.returnUrl.subscribe(url => this.returnUrl = url);

    this.backEndLogin$ = this.afAuth.user.pipe(
      filter(user => !!user),
      tap(firebaseUser => this.firebaseUser = firebaseUser),
      switchMap(user => user.delete()),
      switchMap(_ => this.auth.socialLogin(this.firebaseUser)),
      tap(user => {
        if (user) {
          this.app.setUser(user);
          this.app.returnToUrl(this.returnUrl);
        } else {
          this.loginError('');
        }
      })
    ).subscribe(_ => { }, this.loginError);
  }

  ngOnDestroy() {
    this.backEndLogin$.unsubscribe();
    this.returnUrl$.unsubscribe();
  }

}

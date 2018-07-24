import { Component, OnInit, HostBinding } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import { Router } from '@angular/router';
import { moveIn } from '../../../animations/router.animations';
import * as firebase from 'firebase';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  animations: [moveIn()]
})
export class LoginComponent implements OnInit {

  error: any;
  returnUrl = '';

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
    this.afAuth.user.subscribe(user$ => this.auth.socialLogin(user$), this.loginError);
  }

  loginError = (err) => {
    this.error = 'No se pudo iniciar sesión. Intenta con otro medio.';
    console.warn(err);
  }

  ngOnInit() {
    this.auth.returnUrl.subscribe(url => this.returnUrl = url);
    this.auth.user.subscribe(user$ => {
      if (user$) {
        this.router.navigate([ this.returnUrl ? this.returnUrl : '/members' ]);
      }
    });
  }

}

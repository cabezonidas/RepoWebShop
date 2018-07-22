import { Component, OnInit, HostBinding } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import { Router } from '@angular/router';
import { moveIn } from '../router.animations';
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
  constructor(public afAuth: AngularFireAuth, private router: Router, private auth: AuthService) {
      // if (user) {
      //   this.router.navigateByUrl('/members');
      // }
  }

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
    this.afAuth.user.subscribe(user$ => {
      this.auth.socialLogin(user$);
    });
  }

  loginError = (err) => this.error = err;

  ngOnInit() {
  }

}

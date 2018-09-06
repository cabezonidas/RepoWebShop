import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IAppUser } from '../interfaces/iapp-user';
import { Router } from '@angular/router';
import { AngularFireAuth } from 'angularfire2/auth';
import { UserSocialInfo } from '../classes/UserSocialInfo';
import { EmailRegistration } from '../classes/EmailRegistration';
import { EmailLogin } from '../classes/EmailLogin';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private router: Router, private afAuth: AngularFireAuth) { }

  loadUser = () => (this.http.get('/api/_account/current') as Observable<IAppUser>);

  socialLogin = (user: firebase.User): Observable<IAppUser> => {
    const providerData = new Array<UserSocialInfo>();
    user.providerData.forEach(x => providerData.push(new UserSocialInfo(user, x)));
    return (this.http.post('/api/_account/socialLogIn', providerData) as Observable<IAppUser>);
  }

  registerUserEmail = (emailReg: EmailRegistration, code: string): Observable<IAppUser> => {
    return (this.http.post('/api/_account/registerEmail/' + code, emailReg) as Observable<IAppUser>);
  }

  recoverEmail = (email: string): Observable<any> => {
    return (this.http.post('/api/_account/recoverEmail/' + email, null) as Observable<any>);
  }

  activateRecoveredEmail = (email: string, code: string): Observable<IAppUser> => {
    return (this.http.post('/api/_account/activateRecoveredEmail/' + email + '/' + code, null) as Observable<IAppUser>);
  }

  emailLogin = (user: EmailLogin): Observable<IAppUser> => {
    return (this.http.post('/api/_account/emailLogIn', user) as Observable<IAppUser>);
  }

  logOut = (): Observable<any> => this.http.post('/api/_account/signOut', null) as Observable<any>;

  isEmailAvailable = (email: string): Observable<boolean> =>
    (this.http.get('/api/_account/isEmailAvailable/' + email) as Observable<boolean>)


  bookEmail = (emailReg: EmailRegistration): Observable<any> => (this.http.post('/api/_account/bookEmail', emailReg) as Observable<any>);
  registerMobile = (mobile: string): Observable<any> => (this.http.post('/api/_account/registerMobile/' + mobile, null) as Observable<any>);
  confirmMobile = (code: string): Observable<any> => (this.http.post('/api/_account/confirmMobile/' + code, null) as Observable<any>);
  updatePassword = (pass: string): Observable<any> => (this.http.post('/api/_account/updatePassword/' + pass, null) as Observable<any>);
  isAdmin = (): Observable<boolean> => this.http.get<boolean>('/api/_account/isAdmin/');

}


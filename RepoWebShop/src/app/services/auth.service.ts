import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IAppUser } from '../interfaces/iapp-user';
import { Router } from '@angular/router';
import { AngularFireAuth } from 'angularfire2/auth';
import { UserSocialInfo } from '../classes/UserSocialInfo';
import { EmailLogin } from '../classes/EmailLogin';
import { ValidatorFn, AbstractControl } from '../../../node_modules/@angular/forms';
import { EmailRegistration } from '../classes/EmailRegistration';

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  private returnUrlSource = new BehaviorSubject<string>(null);
  public returnUrl = this.returnUrlSource.asObservable();

  private userSource = new BehaviorSubject<IAppUser>(null);
  public user = this.userSource.asObservable();

  constructor(private http: HttpClient, private router: Router, private afAuth: AngularFireAuth) { }

  loadUser = () => (this.http.get('/api/_account/current') as Observable<IAppUser>).subscribe(user => this.userSource.next(user));

  socialLogin = (user: firebase.User): void => {
    if (user) {
      const providerData = new Array<UserSocialInfo>();
      user.providerData.forEach(x => providerData.push(new UserSocialInfo(user, x)));
      (this.http.post('/api/_account/socialLogIn', providerData) as Observable<any>)
        .subscribe(() => this.loadUser());
    }
  }

  registerUserEmail = (emailReg: EmailRegistration, code: string): Observable<any> => {
    return (this.http.post('/api/_account/registerEmail/' + code, emailReg) as Observable<any>);
  }

  emailLogin = (user: EmailLogin): Observable<IAppUser> => {
    return (this.http.post('/api/_account/emailLogIn', user) as Observable<IAppUser>);
  }

  isAdmin = (): Observable<boolean> => (this.http.get('/api/_account/isAdmin') as Observable<boolean>);
  isAuth = (): Observable<boolean> => (this.http.get('/api/_account/isAuth') as Observable<boolean>);
  isMobileConfirmed = (): Observable<boolean> => (this.http.get('/api/_account/isMobileConfirmed') as Observable<boolean>);

  logOut = (): void => {
    this.userSource.next(null);
    this.afAuth.auth.signOut();
    (this.http.post('/api/_account/signOut', null) as Observable<any>).subscribe(() =>
      this.router.navigateByUrl('/products'));
  }

  setReturnUrl = (url: string) => this.returnUrlSource.next(url);

  isEmailAvailable = (email: string): Observable<boolean> =>
    (this.http.get('/api/_account/isEmailAvailable/' + email) as Observable<boolean>)

  bookEmail = (emailReg: EmailRegistration): Observable<any> =>
    (this.http.post('/api/_account/bookEmail', emailReg) as Observable<any>)
}


import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IAppUser } from '../interfaces/iapp-user';
import { Router } from '@angular/router';
import { AngularFireAuth } from 'angularfire2/auth';
import { UserSocialInfo } from '../classes/UserSocialInfo';

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  private returnUrlSource = new BehaviorSubject<string>(null);
  public returnUrl = this.returnUrlSource.asObservable();

  private userSource = new BehaviorSubject<IAppUser>(null);
  public user = this.userSource.asObservable();

  constructor(private http: HttpClient, private router: Router, private afAuth: AngularFireAuth) { }

  getUser = () => (this.http.get('/api/_account/current') as Observable<IAppUser>).subscribe(user => this.userSource.next(user));

  socialLogin = (user: firebase.User): any => {
    if (user) {
      const providerData = new Array<UserSocialInfo>();
      user.providerData.forEach(x => providerData.push(new UserSocialInfo(user, x)));
      (this.http.post('/api/_account/socialLogIn', providerData) as Observable<IAppUser>).subscribe((appUser) => {
        this.userSource.next(appUser);
        this.returnUrl.subscribe(url => url ? this.router.navigate([ url ]) : this.router.navigate([ '/members' ]));
      });
    }
  }

  isAdmin = (): Observable<boolean> => (this.http.get('/api/_account/isAdmin') as Observable<boolean>);
  isAuth = (): Observable<boolean> => (this.http.get('/api/_account/isAuth') as Observable<boolean>);
  isMobileConfirmed = (): Observable<boolean> => (this.http.get('/api/_account/isMobileConfirmed') as Observable<boolean>);

  logOut = (): Observable<any> => {
    this.afAuth.auth.signOut();
    return this.http.post('/api/_account/signOut', null);
  }

  setReturnUrl = (url: string) => this.returnUrlSource.next(url);
}






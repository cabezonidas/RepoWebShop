import { Injectable } from '@angular/core';
import { Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';
import { IAppUser } from '../../../authentication/interfaces/iapp-user';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class AppService {

  private returnUrlSource = new BehaviorSubject<string>(null);
  public returnUrl = this.returnUrlSource.asObservable();

  private userSource = new BehaviorSubject<IAppUser>(null);
  public user = this.userSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  setReturnUrl = (url: string): void => this.returnUrlSource.next(url);

  returnToUrl = (url: string) => {
    this.returnUrlSource.next(null);
    this.router.navigate([ url ? url : '/profile' ]);
  }

  setUser = (appUser: IAppUser): void => this.userSource.next(appUser);
}

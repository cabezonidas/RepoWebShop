import { Injectable, Optional, Inject } from '@angular/core';
import { Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';
import { IAppUser } from '../../../authentication/interfaces/iapp-user';
import { APP_BASE_HREF } from '@angular/common';


@Injectable({
  providedIn: 'root'
})
export class AppService {
  private returnUrlSource = new BehaviorSubject<string>(null);
  public returnUrl = this.returnUrlSource.asObservable();

  private userSource = new BehaviorSubject<IAppUser>(null);
  public user = this.userSource.asObservable();

  public api = 'api';
  constructor(private router: Router, @Optional() @Inject(APP_BASE_HREF) origin: string) {
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  setReturnUrl = (url: string): void => this.returnUrlSource.next(url);

  returnToUrl = (url: string) => {
    this.returnUrlSource.next(null);
    this.router.navigate([ url ? url : '/profile' ]);
  }

  setUser = (appUser: IAppUser): void => this.userSource.next(appUser);
}

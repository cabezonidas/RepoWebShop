import { Injectable, Optional, Inject } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AppService } from '../app/app.service';
import { tap } from 'rxjs/operators';
import { APP_BASE_HREF } from '@angular/common';


@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  url = '';
  public api = 'api';
  constructor(private router: Router, private app: AppService,
    private http: HttpClient, @Optional() @Inject(APP_BASE_HREF) origin: string)  {
    this.app.returnUrl.subscribe(url => this.url = url);
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  isAuth = (): Observable<boolean> => (this.http.get(this.api + '/_account/isAuth') as Observable<boolean>);

  canActivate(route: ActivatedRouteSnapshot, routerState: RouterStateSnapshot): Observable<boolean> {
    return this.isAuth().pipe(
      tap(state => {
        if (!state) {
          if (!this.url) {
            this.app.setReturnUrl(routerState.url);
          }
          this.router.navigate([ '/login' ]);
      }
    }));
  }
}

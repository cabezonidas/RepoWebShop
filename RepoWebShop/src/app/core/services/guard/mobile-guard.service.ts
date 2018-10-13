import { Injectable, Optional, Inject } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { AppService } from '../app/app.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { APP_BASE_HREF } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class MobileGuardService implements CanActivate {

  url = '';
  public api = 'api';
  constructor(private router: Router, private app: AppService, private http: HttpClient,
    @Optional() @Inject(APP_BASE_HREF) origin: string) {
    this.app.returnUrl.subscribe(url => this.url = url);
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  isMobileConfirmed = (): Observable<boolean> =>
    (this.http.get(this.api + '/_account/isMobileConfirmed') as Observable<boolean>)

  canActivate(route: ActivatedRouteSnapshot, routerState: RouterStateSnapshot): Observable<boolean> {
    return this.isMobileConfirmed().pipe(
      tap(state => {
        if (!state) {
          if (!this.url) {
            this.app.setReturnUrl(routerState.url);
          }
          this.router.navigate([ '/mobile' ]);
      }
    }));
  }
}

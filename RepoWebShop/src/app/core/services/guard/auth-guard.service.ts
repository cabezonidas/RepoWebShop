import { Injectable } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AppService } from '../app/app.service';
import { tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  url = '';
  constructor(private router: Router, private appService: AppService, private http: HttpClient)  {
    this.appService.returnUrl.subscribe(url => this.url = url);
  }

  isAuth = (): Observable<boolean> => (this.http.get('/api/_account/isAuth') as Observable<boolean>);

  canActivate(route: ActivatedRouteSnapshot, routerState: RouterStateSnapshot): Observable<boolean> {
    return this.isAuth().pipe(
      tap(state => {
        if (!state) {
          if (!this.url) {
            this.appService.setReturnUrl(routerState.url);
          }
          this.router.navigate([ '/login' ]);
      }
    }));
  }
}

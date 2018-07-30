import { Injectable } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AppService } from '../app/app.service';
import 'rxjs/add/operator/do';


@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  url = '';
  constructor(private router: Router, private appService: AppService, private http: HttpClient)  {
    console.log('Auth Guard constructor called');
    this.appService.returnUrl.subscribe(url => this.url = url);
  }

  isAuth = (): Observable<boolean> => (this.http.get('/api/_account/isAuth') as Observable<boolean>);

  canActivate(route: ActivatedRouteSnapshot, routerState: RouterStateSnapshot): Observable<boolean> {
    return this.isAuth()
      .do(state => {
        if (!state) {
          if (!this.url) {
            this.appService.setReturnUrl(routerState.url);
          }
          this.router.navigate([ '/login' ]);
      }
    });
  }
}

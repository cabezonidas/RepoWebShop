import { Injectable } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  url = '';
  constructor(private router: Router, private authService: AuthService)  {
    this.authService.returnUrl.subscribe(url => this.url = url);
  }

  canActivate(route: ActivatedRouteSnapshot, routerState: RouterStateSnapshot): Observable<boolean> {
    return this.authService.isAuth()
      .do(state => {
        if (!state) {
          if (!this.url) {
            this.authService.setReturnUrl(routerState.url);
          }
          this.router.navigate([ '/login' ]);
      }
    });
  }
}

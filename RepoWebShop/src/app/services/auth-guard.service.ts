import { Injectable } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import 'rxjs/add/observable/from';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/take';
import { AuthService } from './auth.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private router: Router, private authService: AuthService) { }

  // canActivate(): Observable<boolean> {
  //   return this.auth.user.map(state => !!state).do(authenticated => {
  //       if (!authenticated) {
  //         this.router.navigate([ '/login' ]);
  //   }});}

  canActivate(route: ActivatedRouteSnapshot, routerState: RouterStateSnapshot): Observable<boolean> {
    return this.authService.isAuth()
      .do(state => {
        if (!state) {
          this.authService.setReturnUrl(routerState.url);
          this.router.navigate([ '/login' ]);
      }
    });
  }
}

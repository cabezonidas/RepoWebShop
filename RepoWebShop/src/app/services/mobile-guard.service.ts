import { Injectable } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '../../../node_modules/@angular/router';
import { AuthService } from './auth.service';
import { Observable } from '../../../node_modules/rxjs';
import 'rxjs/add/operator/do';

@Injectable({
  providedIn: 'root'
})
export class MobileGuardService implements CanActivate {

  url = '';
  constructor(private router: Router, private authService: AuthService) {
    this.authService.returnUrl.subscribe(url => this.url = url);
  }

  canActivate(route: ActivatedRouteSnapshot, routerState: RouterStateSnapshot): Observable<boolean> {
    return this.authService.isMobileConfirmed()
      .do(state => {
        if (!state) {
          if (!this.url) {
            this.authService.setReturnUrl(routerState.url);
          }
          this.router.navigate([ '/mobile' ]);
      }
    });
  }
}

import { Injectable } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { AppService } from '../app/app.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/do';

@Injectable({
  providedIn: 'root'
})
export class MobileGuardService implements CanActivate {

  url = '';
  constructor(private router: Router, private appService: AppService, private http: HttpClient) {
    console.log('Mobile Guard constructor called');
    this.appService.returnUrl.subscribe(url => this.url = url);
  }

  isMobileConfirmed = (): Observable<boolean> => (this.http.get('/api/_account/isMobileConfirmed') as Observable<boolean>);

  canActivate(route: ActivatedRouteSnapshot, routerState: RouterStateSnapshot): Observable<boolean> {
    return this.isMobileConfirmed()
      .do(state => {
        if (!state) {
          if (!this.url) {
            this.appService.setReturnUrl(routerState.url);
          }
          this.router.navigate([ '/mobile' ]);
      }
    });
  }
}

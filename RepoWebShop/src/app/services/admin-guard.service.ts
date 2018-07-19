import { Injectable } from '@angular/core';
import { CanActivate, Router } from '../../../node_modules/@angular/router';
import { AuthService } from './auth.service';
import { Observable } from '../../../node_modules/rxjs';
import 'rxjs/add/observable/from';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/take';

@Injectable({
  providedIn: 'root'
})
export class AdminGuardService implements CanActivate {

  constructor(private router: Router, private authService: AuthService) { }


  canActivate(): Observable<boolean> {
    return this.authService.isAdmin()
      .do(isAdmin => {
        console.log('1. Is Admin: ', isAdmin);
        if (!isAdmin) {
          this.router.navigate([ '/login' ]);
      }
    });
  }
}

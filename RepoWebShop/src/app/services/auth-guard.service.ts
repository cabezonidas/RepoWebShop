import { Injectable } from '@angular/core';
import { CanActivate, Router } from '../../../node_modules/@angular/router';
import { AngularFireAuth } from '../../../node_modules/angularfire2/auth';
import { Observable } from '../../../node_modules/rxjs';
import 'rxjs/add/observable/from';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/take';


@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private auth: AngularFireAuth, private router: Router) { }

  canActivate(): Observable<boolean> {
    console.log('[Auth] 1. Can Activate');
    return this.auth.user
      .map(state => !!state)
      .do(authenticated => {
        console.log('[Auth] 2. Authenticated: ', authenticated);
        if (!authenticated) {
          this.router.navigate([ '/login' ]);
      }
    });
  }
}

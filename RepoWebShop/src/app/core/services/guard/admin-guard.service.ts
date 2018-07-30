import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AdminGuardService implements CanActivate {

  constructor(private router: Router, private http: HttpClient) { }

  isAdmin = (): Observable<boolean> => (this.http.get('/api/_account/isAdmin') as Observable<boolean>);

  canActivate(): Observable<boolean> {
    return this.isAdmin().pipe(
      tap(isAdmin => {
        if (!isAdmin) {
          this.router.navigate([ '/products' ]);
      }
    }));
  }
}

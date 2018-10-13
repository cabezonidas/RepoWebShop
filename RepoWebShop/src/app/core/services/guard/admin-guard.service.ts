import { Injectable, Optional, Inject } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { AppService } from '../app/app.service';
import { APP_BASE_HREF } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class AdminGuardService implements CanActivate {

  public api = 'api';
  constructor(private router: Router, private http: HttpClient, @Optional() @Inject(APP_BASE_HREF) origin: string) {
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  isAdmin = (): Observable<boolean> => (this.http.get(this.api + '/_account/isAdmin') as Observable<boolean>);

  canActivate(): Observable<boolean> {
    return this.isAdmin().pipe(
      tap(isAdmin => {
        if (!isAdmin) {
          this.router.navigate([ '/products' ]);
      }
    }));
  }
}

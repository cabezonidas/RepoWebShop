import { Injectable } from '@angular/core';
import { CanLoad, CanActivate } from '@angular/router';
import { Observable, of } from 'rxjs';
import { tap, first } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class HttpsGuardService implements CanLoad, CanActivate {


  isHttps$ = (): Observable<boolean> => (of(window.location.protocol === 'https:'));

  isHttps = () => window.location.protocol === 'https:';
  routeToHttpsIfHttp = () => {
    if (!this.isHttps()) {
      window.location.href = window.location.href.replace('http', 'https');
    }
  }

  canLoad(): boolean {
    console.log('Can Load');
    this.routeToHttpsIfHttp();
    return true;
  }

  canActivate(): boolean {
    console.log('Can Activate');
    this.routeToHttpsIfHttp();
    return true;
  }
}

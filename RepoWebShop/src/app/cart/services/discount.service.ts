import { Injectable, Optional, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { IDiscount } from '../interfaces/idiscount';
import { APP_BASE_HREF } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class DiscountService {

  public api = 'api';
  constructor(private http: HttpClient, @Optional() @Inject(APP_BASE_HREF) origin: string) {
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  get = () => this.http.get<IDiscount>(this.api + '/_cartDiscount/get')
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  apply = (code: string) => this.http.post<IDiscount>(this.api + '/_cartDiscount/apply/' + code, null)
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  clear = () => this.http.delete<void>(this.api + '/_cartDiscount/remove/')
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  exists = (code: string) => this.http.get<boolean>(this.api + '/_cartDiscount/exists/' + code)
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  isActive = (code: string) => this.http.get<boolean>(this.api + '/_cartDiscount/isActive/' + code)
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  isAvailable = (code: string) => this.http.get<boolean>(this.api + '/_cartDiscount/isAvailable/' + code)
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  minOrderReached = (code: string) => this.http.get<boolean>(this.api + '/_cartDiscount/minOrderReached/' + code)
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  isValidToday = (code: string) => this.http.get<boolean>(this.api + '/_cartDiscount/isValidToday/' + code)
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  notExpired = (code: string) => this.http.get<boolean>(this.api + '/_cartDiscount/notExpired/' + code)
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  notPending = (code: string) => this.http.get<boolean>(this.api + '/_cartDiscount/notPending/' + code)
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  isValid = (code: string) => this.http.get<boolean>(this.api + '/_cartDiscount/isValid/' + code)
    .pipe(catchError((error: any) => Observable.throw(error.json())))
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { IDiscount } from '../interfaces/idiscount';

@Injectable({
  providedIn: 'root'
})
export class DiscountService {

  constructor(private http: HttpClient) {}
  
  get = () => this.http.get<IDiscount>('/api/_cartDiscount/get')
    .pipe(catchError((error: any) => Observable.throw(error.json())));

  apply = (code: string) => this.http.post<IDiscount>('/api/_cartDiscount/apply/' + code, null)
    .pipe(catchError((error: any) => Observable.throw(error.json())));

  clear = () => this.http.delete<void>('/api/_cartDiscount/remove/')
    .pipe(catchError((error: any) => Observable.throw(error.json())));

  exists = (code: string) => this.http.get<boolean>('/api/_cartDiscount/exists/' + code)
    .pipe(catchError((error: any) => Observable.throw(error.json())));

  isActive = (code: string) => this.http.get<boolean>('/api/_cartDiscount/isActive/' + code)
    .pipe(catchError((error: any) => Observable.throw(error.json())));

  isAvailable = (code: string) => this.http.get<boolean>('/api/_cartDiscount/isAvailable/' + code)
    .pipe(catchError((error: any) => Observable.throw(error.json())));

  minOrderReached = (code: string) => this.http.get<boolean>('/api/_cartDiscount/minOrderReached/' + code)
    .pipe(catchError((error: any) => Observable.throw(error.json())));

  isValidToday = (code: string) => this.http.get<boolean>('/api/_cartDiscount/isValidToday/' + code)
    .pipe(catchError((error: any) => Observable.throw(error.json())));

  notExpired = (code: string) => this.http.get<boolean>('/api/_cartDiscount/notExpired/' + code)
    .pipe(catchError((error: any) => Observable.throw(error.json())));

  notPending = (code: string) => this.http.get<boolean>('/api/_cartDiscount/notPending/' + code)
    .pipe(catchError((error: any) => Observable.throw(error.json())));

  isValid = (code: string) => this.http.get<boolean>('/api/_cartDiscount/isValid/' + code)
    .pipe(catchError((error: any) => Observable.throw(error.json())));
}

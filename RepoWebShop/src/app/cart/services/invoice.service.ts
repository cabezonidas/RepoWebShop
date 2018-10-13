import { Injectable, Optional, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { IDiscount } from '../interfaces/idiscount';
import { APP_BASE_HREF } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  public api = 'api';
  constructor(private http: HttpClient, @Optional() @Inject(APP_BASE_HREF) origin: string) {
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  isCuitValid = (cuit: string) => this.http.get<boolean>(this.api + '/_cartInvoice/isCuitValid/' + cuit)
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  addCuit = (cuit: string) => {
    return this.http.post<string>(this.api + '/_cartInvoice/addCuit/' + cuit, null)
    .pipe(catchError((error: any) => Observable.throw(error.json())));
  }
  clearCuit = () => this.http.delete<void>(this.api + '/_cartInvoice/clearCuit/')
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  getCuit = () => this.http.get<string>(this.api + '/_cartInvoice/getCuit/')
    .pipe(catchError((error: any) => Observable.throw(error.json())))
}

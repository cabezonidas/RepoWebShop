import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { IDiscount } from '../interfaces/idiscount';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  constructor(private http: HttpClient) {}

  isCuitValid = (cuit: string) => this.http.get<boolean>('/api/_cartInvoice/isCuitValid/' + cuit)
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  addCuit = (cuit: string) => {
    return this.http.post<string>('/api/_cartInvoice/addCuit/' + cuit, null)
    .pipe(catchError((error: any) => Observable.throw(error.json())));
  }
  clearCuit = () => this.http.delete<void>('/api/_cartInvoice/clearCuit/')
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  getCuit = () => this.http.get<string>('/api/_cartInvoice/getCuit/')
    .pipe(catchError((error: any) => Observable.throw(error.json())))
}

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  constructor(private http: HttpClient) { }

  getMercadoPagoLink = () => this.http.get<any>('/api/ShoppingCartData/GetMercadoPagoLink/')
    .pipe(catchError((error: any) => Observable.throw(error.json())))
}

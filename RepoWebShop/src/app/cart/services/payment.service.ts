import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  constructor(private http: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json'
    }),
    responseType: 'text' as 'text'
  };


  getMercadoPagoLink = () => this.http.get<any>('/api/ShoppingCartData/GetMercadoPagoLink/')
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  confirmReservaion = () => this.http.post('/api/_cartCheckout/confirmReservation/', null, this.httpOptions);
}

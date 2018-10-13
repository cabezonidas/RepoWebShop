import { Injectable, Optional, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { APP_BASE_HREF } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  public api = 'api';
  constructor(private http: HttpClient, @Optional() @Inject(APP_BASE_HREF) origin: string) {
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json'
    }),
    responseType: 'text' as 'text'
  };


  getMercadoPagoLink = () => this.http.get<any>(this.api + '/ShoppingCartData/GetMercadoPagoLink/')
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  confirmReservaion = () => this.http.post(this.api + '/_cartCheckout/confirmReservation/', null, this.httpOptions);
}

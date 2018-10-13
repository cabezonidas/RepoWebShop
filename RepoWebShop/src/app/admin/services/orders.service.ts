import { Injectable, Optional, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IOrder } from '../interfaces/iorder';
import { IOrderCatering } from '../interfaces/iorder-catering';
import { IOrderItem } from '../interfaces/iorder-item';
import { IOrderPie } from '../interfaces/iorder-pie';
import { IAmountTitle } from '../interfaces/iamount-title';
import { IInvoiceData } from '../interfaces/iinvoice-data';
import { IDiscount } from '../../cart/interfaces/idiscount';
import { APP_BASE_HREF } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  public api = 'api';
  constructor(private http: HttpClient, @Optional() @Inject(APP_BASE_HREF) origin: string) {
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  getInProgress = () => this.http.get<IOrder[]>(this.api + '/_orders/inProgress');
  getAll = () => this.http.get<IOrder[]>(this.api + '/_orders/all');
  getOrderItems = (id: number) => this.http.get<IOrderItem[]>(this.api + '/_orders/items/' + id);
  getCaterings = (id: number) => this.http.get<IOrderCatering[]>(this.api + '/_orders/caterings/' + id);
  getOrderPies = (id: number) => this.http.get<IOrderPie[]>(this.api + '/_orders/pies/' + id);

  getOrderBreakDown = (id: number) => this.http.get<IAmountTitle[]>(this.api + '/_orders/detailsBreakDown/' + id);

  getCustomerName = (id: number) => this.http.get(this.api + '/_orders/customerName/' + id, {responseType: 'text'});
  getCustomerEmails = (id: number) => this.http.get(this.api + '/_orders/customerEmails/' + id, {responseType: 'text'});
  getCustomerNumbers = (id: number) => this.http.get(this.api + '/_orders/customerNumbers/' + id, {responseType: 'text'});


  getBilling = (id: number) => this.http.get<IInvoiceData>(this.api + '/_orders/invoiceData/' + id);
  getDiscount = (id: number) => this.http.get<IDiscount>(this.api + '/_orders/discount/' + id);

  archive = (id: number) => this.http.post<void>(this.api + '/OrderData/PickUpOrder/' + id, null);
  done = (id: number) => this.http.post<void>(this.api + '/OrderData/CompleteOrder/' + id, null);

  printTicket = (id: number) => this.http.post<void>(this.api + '/OrderData/PrintOnlineOrder/' + id, null);
}

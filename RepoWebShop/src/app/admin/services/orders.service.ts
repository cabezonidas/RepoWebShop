import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IOrder } from '../interfaces/iorder';
import { IOrderCatering } from '../interfaces/iorder-catering';
import { IOrderItem } from '../interfaces/iorder-item';
import { IOrderPie } from '../interfaces/iorder-pie';
import { IAmountTitle } from '../interfaces/iamount-title';
import { IInvoiceData } from '../interfaces/iinvoice-data';
import { IDiscount } from '../../cart/interfaces/idiscount';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(private http: HttpClient) { }

  getInProgress = () => this.http.get<IOrder[]>('/api/_orders/inProgress');
  getAll = () => this.http.get<IOrder[]>('/api/_orders/all');
  getOrderItems = (id: number) => this.http.get<IOrderItem[]>('/api/_orders/items/' + id);
  getCaterings = (id: number) => this.http.get<IOrderCatering[]>('/api/_orders/caterings/' + id);
  getOrderPies = (id: number) => this.http.get<IOrderPie[]>('/api/_orders/pies/' + id);

  getOrderBreakDown = (id: number) => this.http.get<IAmountTitle[]>('/api/_orders/detailsBreakDown/' + id);

  getCustomerName = (id: number) => this.http.get('/api/_orders/customerName/' + id, {responseType: 'text'});
  getCustomerEmails = (id: number) => this.http.get('/api/_orders/customerEmails/' + id, {responseType: 'text'});
  getCustomerNumbers = (id: number) => this.http.get('/api/_orders/customerNumbers/' + id, {responseType: 'text'});


  getBilling = (id: number) => this.http.get<IInvoiceData>('/api/_orders/invoiceData/' + id);
  getDiscount = (id: number) => this.http.get<IDiscount>('/api/_orders/discount/' + id);

  archive = (id: number) => this.http.post<void>('/api/OrderData/PickUpOrder/' + id, null);
  done = (id: number) => this.http.post<void>('/api/OrderData/CompleteOrder/' + id, null);

  printTicket = (id: number) => this.http.post<void>('/api/OrderData/PrintOnlineOrder/' + id, null);
}

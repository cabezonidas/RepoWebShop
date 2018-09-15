import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IOrder } from '../interfaces/iorder';
import { IOrderCatering } from '../interfaces/iorder-catering';
import { IOrderItem } from '../interfaces/iorder-item';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(private http: HttpClient) { }

  getInProgress = () => this.http.get<IOrder[]>('/api/_orders/inProgress');
  getOrderItems = (id: number) => this.http.get<IOrderItem[]>('/api/_orders/items/' + id);
  getCaterings = (id: number) => this.http.get<IOrderCatering[]>('/api/_orders/caterings/' + id);
}

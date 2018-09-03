import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProduct } from '../interfaces/iproduct';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http: HttpClient) { }

  getProducts = (): Observable<IProduct[]> => this.http.get<IProduct[]>('/api/_products/all');
  getPickup = (hours: number): Observable<Date> => this.http.get<Date>('/api/_calendar/readyFor/' + hours);
}

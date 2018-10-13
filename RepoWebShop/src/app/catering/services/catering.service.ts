import { Injectable, Optional, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IItem } from '../../products/interfaces/iitem';
import { ICatering } from '../interfaces/ICatering';
import { APP_BASE_HREF } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class CateringService {

  public api = 'api';
  constructor(private http: HttpClient, @Optional() @Inject(APP_BASE_HREF) origin: string) {
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  getItems = (): Observable<IItem[]> => this.http.get<IItem[]>(this.api + '/_cartCustomCatering/cateringItems');

  loadSessionCatering = (): Observable<ICatering> =>
    this.http.get<ICatering>(this.api + '/_cartCustomCatering/loadSessionCatering')

  loadCaterings = (): Observable<ICatering[]> => this.http.get<ICatering[]>(this.api + '/_caterings/availableCaterings');

  addItem = (productId: number): Observable<ICatering> =>
    this.http.post<ICatering>(this.api + '/_cartCustomCatering/addItem/' + productId, null)

  removeItem = (productId: number): Observable<ICatering> =>
    this.http.delete<ICatering>(this.api + '/_cartCustomCatering/removeItem/' + productId)

  clearItem = (productId: number): Observable<ICatering> =>
    this.http.delete<ICatering>(this.api + '/_cartCustomCatering/clearItem/' + productId)

  totalInStore = (catering: ICatering): number => {
    let subtotal = 0;
    if (catering && catering.items && catering.miscellanea) {
      catering.items.forEach(i => subtotal += i.subTotalInStore);
      catering.miscellanea.forEach(i => subtotal += i.price);
    }
    return subtotal;
  }
  totalOnline = (catering: ICatering): number => {
    let subtotal = 0;
    if (catering && catering.items && catering.miscellanea) {
      catering.items.forEach(i => subtotal += i.subTotal);
      catering.miscellanea.forEach(i => subtotal += i.price);
    }
    return subtotal;
  }
}

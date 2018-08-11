import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IItem } from '../../products/interfaces/iitem';
import { ICatering } from '../../catering/interfaces/ICatering';

@Injectable({
  providedIn: 'root'
})
export class CustomCateringService {

  constructor(private http: HttpClient) { }

  getItems = (): Observable<IItem[]> => this.http.get<IItem[]>('/api/_cartCustomCatering/cateringItems');
  loadSessionCatering = (): Observable<ICatering> => this.http.get<ICatering>('/api/_cartCustomCatering/loadSessionCatering');
  loadCaterings = (): Observable<ICatering[]> => this.http.get<ICatering[]>('/api/_cartCustomCatering/availableCaterings');
  addItem = (productId: number): Observable<ICatering> => this.http.post<ICatering>('/api/_cat_cartCustomCateringering/addItem/' + productId, null);
  removeItem = (productId: number): Observable<ICatering> => this.http.delete<ICatering>('/api/_cartCustomCatering/removeItem/' + productId);
  clearItem = (productId: number): Observable<ICatering> => this.http.delete<ICatering>('/api/_cartCustomCatering/clearItem/' + productId);
}
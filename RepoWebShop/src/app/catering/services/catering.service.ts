import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IItem } from '../../products/interfaces/iitem';
import { ICatering } from '../interfaces/ICatering';

@Injectable({
  providedIn: 'root'
})
export class CateringService {

  constructor(private http: HttpClient) { }

  getItems = (): Observable<IItem[]> => this.http.get<IItem[]>('/api/_cartCustomCatering/cateringItems');

  loadSessionCatering = (): Observable<ICatering> => this.http.get<ICatering>('/api/_cartCustomCatering/loadSessionCatering');

  loadCaterings = (): Observable<ICatering[]> => this.http.get<ICatering[]>('/api/_caterings/availableCaterings');

  addItem = (productId: number): Observable<ICatering> => this.http.post<ICatering>('/api/_cartCustomCatering/addItem/' + productId, null);

  removeItem = (productId: number): Observable<ICatering> =>
    this.http.delete<ICatering>('/api/_cartCustomCatering/removeItem/' + productId)

  clearItem = (productId: number): Observable<ICatering> => this.http.delete<ICatering>('/api/_cartCustomCatering/clearItem/' + productId);
}

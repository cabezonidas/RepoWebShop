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

  loadSessionCatering = (): Observable<ICatering> => this.http.get<ICatering>('/api/_cartCustomCatering/loadSessionCatering');
  clearSessionCatering = (): Observable<void> => this.http.delete<void>('/api/_cartCustomCatering/clearSessionCatering');
}

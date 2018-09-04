import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICartCatering } from '../interfaces/icart-catering';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ICatering } from '../../catering/interfaces/ICatering';

@Injectable({
  providedIn: 'root'
})
export class CateringsService {

  constructor(private http: HttpClient) {}

  getCartCaterings = () => this.http.get<ICartCatering[]>('/api/_cartCaterings/all')
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  addCatering = (cateringId: number) => this.http.post<ICartCatering[]>('/api/_cartCaterings/add', cateringId)
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  removeCatering = (cateringId: number) => this.http.post<ICartCatering[]>('/api/_cartCaterings/remove', cateringId)
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  copyCatering = (cateringId: number) => this.http.post<ICatering>('/api/_cartCustomCatering/copyCatering/' + cateringId, null)
    .pipe(catchError((error: any) => Observable.throw(error.json())))
}

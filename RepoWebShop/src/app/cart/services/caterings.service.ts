import { Injectable, Optional, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICartCatering } from '../interfaces/icart-catering';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ICatering } from '../../catering/interfaces/ICatering';
import { APP_BASE_HREF } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class CateringsService {

  public api = 'api';
  constructor(private http: HttpClient, @Optional() @Inject(APP_BASE_HREF) origin: string) {
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  getCartCaterings = () => this.http.get<ICartCatering[]>(this.api + '/_cartCaterings/all')
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  addCatering = (cateringId: number) => this.http.post<ICartCatering[]>(this.api + '/_cartCaterings/add', cateringId)
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  removeCatering = (cateringId: number) => this.http.post<ICartCatering[]>(this.api + '/_cartCaterings/remove', cateringId)
    .pipe(catchError((error: any) => Observable.throw(error.json())))

  copyCatering = (cateringId: number) =>
    this.http.post<ICatering>(this.api + '/_cartCustomCatering/copyCatering/' + cateringId, null)
    .pipe(catchError((error: any) => Observable.throw(error.json())))
}

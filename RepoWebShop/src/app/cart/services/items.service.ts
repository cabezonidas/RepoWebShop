import { Injectable, Optional, Inject } from '@angular/core';
import { ICartCatalogItem } from '../interfaces/icart-catalog-item';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { APP_BASE_HREF } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class ItemsService {

  public api = 'api';
  constructor(private http: HttpClient, @Optional() @Inject(APP_BASE_HREF) origin: string) {
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  getCartItems = () =>
    this.http.get<ICartCatalogItem[]>(this.api + '/_cartItems/getProductItems')
      .pipe(catchError((error: any) => Observable.throw(error.json())))

  addItem = (productId: number) =>
    this.http.post<ICartCatalogItem[]>(this.api + '/_cartItems/addProductItem', productId)
      .pipe(catchError((error: any) => Observable.throw(error.json())))

  removeItem = (productId: number) =>
    this.http.post<ICartCatalogItem[]>(this.api + '/_cartItems/removeProductItem', productId)
      .pipe(catchError((error: any) => Observable.throw(error.json())))
}

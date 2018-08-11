import { Injectable } from '@angular/core';
import { ICartCatalogItem } from '../interfaces/icart-catalog-item';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ItemsService {

  constructor(private http: HttpClient) {}
  
  getCartItems = () => 
    this.http.get<ICartCatalogItem[]>('/api/_cartItems/getProductItems')
      .pipe(catchError((error: any) => Observable.throw(error.json())));
  
  addItem = (productId: number) => 
    this.http.post<ICartCatalogItem[]>('/api/_cartItems/addProductItem', productId)
      .pipe(catchError((error: any) => Observable.throw(error.json())));
  
  removeItem = (productId: number) => 
    this.http.post<ICartCatalogItem[]>('/api/_cartItems/removeProductItem', productId)
      .pipe(catchError((error: any) => Observable.throw(error.json())));
}

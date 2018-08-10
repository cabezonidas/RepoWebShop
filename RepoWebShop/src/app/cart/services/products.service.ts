import { Injectable } from '@angular/core';
import { ICartCatalogItem } from '../interfaces/icart-catalog-item';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http: HttpClient) {}
  
  getCartItems = () => 
    this.http.get<ICartCatalogItem[]>('/api/_shoppingCart/getProductItems')
      .pipe(catchError((error: any) => Observable.throw(error.json())));
  
  addProduct = (productId: number) => 
    this.http.post<ICartCatalogItem[]>('/api/_shoppingCart/addProductItem', productId)
      .pipe(catchError((error: any) => Observable.throw(error.json())));
  
  removeProduct = (productId: number) => 
    this.http.post<ICartCatalogItem[]>('/api/_shoppingCart/removeProductItem', productId)
      .pipe(catchError((error: any) => Observable.throw(error.json())));
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICartCatalogItem } from '../interfaces/icart-catalog-item';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  constructor(private http: HttpClient) { }

  addProductToCart = (id) => this.http.post('/api/ShoppingCartData/AddProductItem', id);

  getProducts = () => this.http.get('/api/ShoppingCartData/GetProductItems') as Observable<Array<ICartCatalogItem>>;
}

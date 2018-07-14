import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICartCatalogItem } from '../interfaces/icart-catalog-item';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private productsSource = new BehaviorSubject<Array<ICartCatalogItem>>([]);
  public currentProducts = this.productsSource.asObservable();

  constructor(private http: HttpClient) { }

  addProductToCart = (id) => this.http.post('/api/ShoppingCartData/AddProductItem', id).subscribe(() => this.getProducts());

  getProducts = () => {
    (this.http.get('/api/ShoppingCartData/GetProductItems') as Observable<Array<ICartCatalogItem>>).subscribe(
        products => this.productsSource.next(products)
    );
  }
}

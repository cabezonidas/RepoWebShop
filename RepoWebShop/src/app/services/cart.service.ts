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

  addProductToCart = (id) => this.http.post('/api/_shoppingcart/addproductitem', id).subscribe(() => this.getProducts());
  removeProductFromCart = (id) => this.http.post('/api/_shoppingcart/removeproductitem', id).subscribe(() => this.getProducts());
  clearProductFromCart = (id) => this.http.post('/api/_shoppingcart/clearproductitem', id).subscribe(() => this.getProducts());

  getProducts = () => {
    (this.http.get('/api/_shoppingcart/getproductitems') as Observable<Array<ICartCatalogItem>>).subscribe(
        products => this.productsSource.next(products)
    );
  }
}

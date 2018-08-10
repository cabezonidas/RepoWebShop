import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { ICartCatalogItem } from '../interfaces/icart-catalog-item';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private productsSource = new BehaviorSubject<Array<ICartCatalogItem>>([]);
  public currentProducts = this.productsSource.asObservable();

  constructor(private http: HttpClient) { }

  // Podria implementar aca los totals
  setCartItems = (products: Array<ICartCatalogItem>): void => this.productsSource.next(products);

  addProductToCart = (id) => this.http.post('/api/_shoppingcart/addproductitem', id) as Observable<Array<ICartCatalogItem>>;
  removeProductFromCart = (id) => this.http.post('/api/_shoppingcart/removeproductitem', id) as Observable<Array<ICartCatalogItem>>;
  clearProductFromCart = (id) => this.http.post('/api/_shoppingcart/clearproductitem', id) as Observable<Array<ICartCatalogItem>>;
  getProducts = () => (this.http.get('/api/_shoppingcart/getproductitems') as Observable<Array<ICartCatalogItem>>);
}

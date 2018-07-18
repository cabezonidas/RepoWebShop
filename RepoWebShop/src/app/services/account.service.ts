import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  // private productsSource = new BehaviorSubject<Array<ICartCatalogItem>>([]);
  // public currentProducts = this.productsSource.asObservable();

  constructor(private http: HttpClient) { }

  externalLoginProviders = () => this.http.get('/api/_account/externalloginproviders') as Observable<Array<string>>;

  externalLogin = (provider: string) => this.http.post('/api/_account/externallogin/' + provider, { provider }) as Observable<any>;
  // getProducts = () => {
  //   (this.http.get('/api/_shoppingcart/getproductitems') as Observable<Array<ICartCatalogItem>>).subscribe(
  //       products => this.productsSource.next(products)
  //   );
  // }
}

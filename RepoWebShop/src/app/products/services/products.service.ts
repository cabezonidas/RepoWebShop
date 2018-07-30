import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IProduct } from '../interfaces/iproduct';



@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http: HttpClient) { }

  getProducts() {
    return this.http.get('/api/_products/all') as Observable<Array<IProduct>>;
  }
  getProduct(id) {
    return this.http.get('/api/_products/all/' + id);
  }
}

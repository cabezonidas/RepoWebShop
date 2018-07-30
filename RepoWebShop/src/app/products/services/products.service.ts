import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProduct } from '../interfaces/iproduct';
import { tap, catchError } from 'rxjs/operators';



@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http: HttpClient) { }

  getProducts() {
    return this.http.get<IProduct[]>('/api/_products/all').pipe(
      // tap(data => console.log(JSON.stringify(data)))
      // catchError((err) => this.handleError(err))
    );
  }
  getProduct(id) {
    return this.http.get('/api/_products/all/' + id);
  }

  handleError = (err) => console.log(err);

}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProduct } from '../interfaces/iproduct';
import { Observable } from 'rxjs';
import { IItem } from '../interfaces/iitem';


@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http: HttpClient) { }

  getProducts = (): Observable<IProduct[]> => this.http.get<IProduct[]>('/api/_products/all');
  compare = (a: IItem, b: IItem): number =>
    (this.rankCategory(a.category) + a.displayName < this.rankCategory(b.category) + b.displayName ? -1 : 1)

  reverseCompare = (a: IItem, b: IItem): number =>
    (this.reverseRankCategory(a.category) + a.displayName < this.reverseRankCategory(b.category) + b.displayName ? -1 : 1)

  rankCategory = (category: string): number => {
    let result = 9;
    category = category.toLowerCase().trim();
    switch (category) {
      case 'postre': {
        result = 1;
        break;
      }
      case 'seco': {
        result = 2;
        break;
      }
      case 'lunch': {
        result = 3;
        break;
      }
      case 'appetizer': {
        result = 4;
        break;
      }
      default: {
        break;
      }
    }
    return result;
  }
  reverseRankCategory = (category: string) => 10 - this.rankCategory(category);
}

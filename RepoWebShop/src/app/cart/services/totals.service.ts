import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ITotals } from '../interfaces/itotals';

@Injectable({
  providedIn: 'root'
})
export class TotalsService {

  constructor(private http: HttpClient) { }

  // total = (): Observable<number> => this.http.get<number>('/api/_cartTotals/total');
  // totalInStore = (): Observable<number> => this.http.get<number>('/api/_cartTotals/totalInStore');
  
  // totalWithoutDiscount = (): Observable<number> => this.http.get<number>('/api/_cartTotals/totalWithoutDiscount');
  
  // productsTotal = (): Observable<number> => this.http.get<number>('/api/_cartTotals/productsTotal');
  // productsTotalInStore = (): Observable<number> => this.http.get<number>('/api/_cartTotals/productsTotalInStore');
  
  // customCateringTotal = (): Observable<number> => this.http.get<number>('/api/_cartTotals/customCateringTotal');
  // customCateringTotalInStore = (): Observable<number> => this.http.get<number>('/api/_cartTotals/customCateringTotalInStore');
  
  // cateringsTotal = (): Observable<number> => this.http.get<number>('/api/_cartTotals/cateringsTotal');
  // cateringsTotalInStore = (): Observable<number> => this.http.get<number>('/api/_cartTotals/cateringsTotalInStore');
  // cateringsTotalSavings = (): Observable<number> => this.http.get<number>('/api/_cartTotals/cateringsTotalSavings');

  getTotals = (): Observable<ITotals> => this.http.get<ITotals>('/api/_cartTotals/totals');
}

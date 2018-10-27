import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IItem } from '../../products/interfaces/iitem';
import { ICatering } from '../interfaces/ICatering';
import { ICateringItem } from '../interfaces/ICateringItem';

@Injectable({
  providedIn: 'root'
})
export class CateringService {

  constructor(private http: HttpClient) { }

  getItems = (): Observable<IItem[]> => this.http.get<IItem[]>('/api/_cartCustomCatering/cateringItems');

  loadSessionCatering = (): Observable<ICatering> => this.http.get<ICatering>('/api/_cartCustomCatering/loadSessionCatering');

  loadCaterings = (): Observable<ICatering[]> => this.http.get<ICatering[]>('/api/_caterings/availableCaterings');

  addItem = (productId: number): Observable<ICatering> => this.http.post<ICatering>('/api/_cartCustomCatering/addItem/' + productId, null);

  removeItem = (productId: number): Observable<ICatering> =>
    this.http.delete<ICatering>('/api/_cartCustomCatering/removeItem/' + productId)

  clearItem = (productId: number): Observable<ICatering> => this.http.delete<ICatering>('/api/_cartCustomCatering/clearItem/' + productId);

  totalInStore = (catering: ICatering): number => {
    let subtotal = 0;
    if (catering && catering.items && catering.miscellanea) {
      catering.items.forEach(i => subtotal += i.subTotalInStore);
      catering.miscellanea.forEach(i => subtotal += i.price);
    }
    return subtotal;
  }
  totalOnline = (catering: ICatering): number => {
    let subtotal = 0;
    if (catering && catering.items && catering.miscellanea) {
      catering.items.forEach(i => subtotal += i.subTotal);
      catering.miscellanea.forEach(i => subtotal += i.price);
    }
    return subtotal;
  }

  groupItems = (items: IItem[]): ICateringItem[] => {
    const cateringItems: ICateringItem[] = [];
    items.forEach(item => {
      const index = cateringItems.findIndex((i) => i.item.productId === item.productId);
      if (index >= 0) {
        cateringItems[index] = this.addICateringItemInstance(cateringItems[index], item);
      } else {
        cateringItems.push(this.addICateringItemInstance(null, item));
      }
    });
    return cateringItems;
  }

  itemCount = (item: IItem, quantity: number): number => {
    const result = quantity <= 0 ? 0 :
    (quantity === 1 ? item.minOrderAmount : item.minOrderAmount + ((quantity - 1) * item.multipleAmount));
    return result;
  }

  calculateCountTotals = (catItem: ICateringItem, quantity: number): ICateringItem => {
    catItem.quantity = quantity >= 0 ? quantity : 0;
    catItem.itemCount = this.itemCount(catItem.item, catItem.quantity);
    catItem.subTotal = catItem.itemCount * catItem.item.price;
    catItem.subTotalInStore = catItem.itemCount * catItem.item.priceInStore;
    return catItem;
  }

  addICateringItemInstance = (cateringItem: ICateringItem, item: IItem): ICateringItem => {
    if (!cateringItem) {
      const quantity = 1;
      const itemCount = this.itemCount(item, quantity);
      cateringItem = <ICateringItem> {
        item: item,
        quantity: quantity,
        itemCount: itemCount,
        subTotal: itemCount * item.price,
        subTotalInStore: itemCount * item.priceInStore
      };
    } else {
      cateringItem = this.calculateCountTotals(cateringItem, cateringItem.quantity += 1);
    }
    return cateringItem;
  }
}

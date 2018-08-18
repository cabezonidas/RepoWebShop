import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ICartItem } from '../../interfaces/icart-item';
import { ICartCatering } from '../../interfaces/icart-catering';
import { ICatering } from '../../../catering/interfaces/ICatering';
import { ICartCatalogItem } from '../../interfaces/icart-catalog-item';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  @Input() items: ICartCatalogItem[];
  @Input() caterings: ICartCatering[];
  @Input() customCatering: ICatering;
  @Input() itemsLoaded: boolean;
  @Input() itemsLoading: boolean;
  
  @Output() next = new EventEmitter<void>();
  @Output() addItem = new EventEmitter<number>();
  @Output() removeItem = new EventEmitter<number>();
  @Output() addCatering = new EventEmitter<number>();
  @Output() removeCatering = new EventEmitter<number>();
  @Output() removeCustomCatering = new EventEmitter<void>();
  
  constructor() { }

  ngOnInit() {
  }
  
  continue = () => this.next.emit();
  addItemToCart = (id: number) => this.addItem.emit(id);
  removeItemFromCart = (id: number) => this.removeItem.emit(id);
  addCateringToCart = (id: number) => this.addCatering.emit(id);
  removeCateringFromCart = (id: number) => this.removeCatering.emit(id);
  removeCustomCateringFromCart = () => this.removeCustomCatering.emit();
  
  getSum = (total, num) => total + num;

  customCateringPrice = (): number => {
    let subTotalItems = 0;
    if (this.customCatering && (this.customCatering.items.length || this.customCatering.miscellanea.length)) {
      this.customCatering.items.forEach(i => subTotalItems += i.subTotal);
      this.customCatering.miscellanea.forEach(i => subTotalItems += i.price);
    }
    return subTotalItems;
  }

  customCateringPriceInStore = (): number => {
    let subTotalItems = 0;
    if (this.customCatering && (this.customCatering.items.length || this.customCatering.miscellanea.length)) {
      this.customCatering.items.forEach(i => subTotalItems += i.subTotalInStore);
      this.customCatering.miscellanea.forEach(i => subTotalItems += i.price);
    }
    return subTotalItems;
  }
}

import { Component, OnInit, Input, OnChanges, OnDestroy, EventEmitter, Output } from '@angular/core';
import { IItem } from '../../../products/interfaces/iitem';
import { ICatering } from '../../../catering/interfaces/ICatering';
import { IOrderItem } from '../../interfaces/iorder-item';
import { IOrderCatering } from '../../interfaces/iorder-catering';
import { OrdersService } from '../../services/orders.service';
import { Observable, Subscription } from 'rxjs';
import { IOrder } from '../../interfaces/iorder';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.scss']
})
export class OrderDetailsComponent implements OnInit, OnChanges, OnDestroy {

  @Input() orderId: number;
  @Input() orderReady: boolean;

  @Output() save = new EventEmitter();
  @Output() notify = new EventEmitter();

  itemSub = new Subscription();
  cateSub = new Subscription();

  caterings: IOrderCatering[];
  items: IOrderItem[];
  constructor(private orderService: OrdersService) { }

  ngOnInit() {
    this.itemSub = this.orderService.getOrderItems(this.orderId).subscribe(items => this.items = items);
    this.cateSub = this.orderService.getCaterings(this.orderId).subscribe(caterings => this.caterings = caterings);
  }

  ngOnChanges() {
  }

  ngOnDestroy() {
    this.itemSub.unsubscribe();
    this.cateSub.unsubscribe();
  }

}

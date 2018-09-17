import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { IOrder } from '../../interfaces/iorder';
import { OrdersService } from '../../services/orders.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-order-customer',
  templateUrl: './order-customer.component.html',
  styleUrls: ['./order-customer.component.scss']
})
export class OrderCustomerComponent implements OnInit, OnDestroy {

  @Input() orderId: number;
  customerName: string;
  nameSub = new Subscription();
  constructor(private orders: OrdersService) { }

  ngOnInit() {
    this.nameSub = this.orders.getCustomerName(this.orderId).subscribe(name => this.customerName = name.toLowerCase());
  }

  ngOnDestroy() {
    this.nameSub.unsubscribe();
  }
}

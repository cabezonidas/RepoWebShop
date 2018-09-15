import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IOrder } from '../../interfaces/iorder';
import { OrdersService } from '../../services/orders.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-active-orders-shell',
  templateUrl: './active-orders-shell.component.html',
  styleUrls: ['./active-orders-shell.component.scss']
})
export class ActiveOrdersShellComponent implements OnInit {

  orders$: Observable<IOrder[]>;
  constructor(private orders: OrdersService) { }

  ngOnInit() {
    this.orders$ = this.orders.getInProgress().pipe(
      map(orders => orders.sort((a, b) => (new Date(a.pickUpTimeFrom)).valueOf() - (new Date(b.pickUpTimeFrom)).valueOf()))
    );
  }
}

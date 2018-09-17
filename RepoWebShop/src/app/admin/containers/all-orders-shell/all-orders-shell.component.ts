import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IOrder } from '../../interfaces/iorder';
import { OrdersService } from '../../services/orders.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-all-orders-shell',
  templateUrl: './all-orders-shell.component.html',
  styleUrls: ['./all-orders-shell.component.scss']
})
export class AllOrdersShellComponent implements OnInit {

  orders$: Observable<IOrder[]>;
  constructor(private orders: OrdersService) { }

  ngOnInit() {
    this.orders$ = this.orders.getAll().pipe(
      map(orders => orders.sort((a, b) => (new Date(a.pickUpTimeFrom)).valueOf() - (new Date(b.pickUpTimeFrom)).valueOf()))
    );
  }

}

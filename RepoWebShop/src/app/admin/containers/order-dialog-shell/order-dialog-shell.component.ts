import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
import { IOrder } from '../../interfaces/iorder';
import { Observable } from 'rxjs';
import { DeliveryAddress } from '../../../cart/classes/delivery-address';
import { OrdersService } from '../../services/orders.service';
import { IAmountTitle } from '../../interfaces/iamount-title';
import { DeliveryService } from '../../services/delivery.service';
import { IDiscount } from '../../../cart/interfaces/idiscount';

@Component({
  selector: 'app-order-dialog-shell',
  templateUrl: './order-dialog-shell.component.html',
  styleUrls: ['./order-dialog-shell.component.scss']
})
export class OrderDialogShellComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: IOrder, private orders: OrdersService, private delivery: DeliveryService) {}

  deliveryAddress$: Observable<DeliveryAddress>;
  customerName$: Observable<string>;
  customerNumbers$: Observable<string>;
  customerEmails$: Observable<string>;
  discount$: Observable<IDiscount>;
  detailsBreakDown$: Observable<IAmountTitle[]>;


  ngOnInit() {
    this.customerName$ = this.orders.getCustomerName(this.data.orderId);
    this.customerNumbers$ = this.orders.getCustomerNumbers(this.data.orderId);
    this.customerEmails$ = this.orders.getCustomerEmails(this.data.orderId);
    this.detailsBreakDown$ = this.orders.getOrderBreakDown(this.data.orderId);
    this.discount$ = this.orders.getDiscount(this.data.discountId);
    this.deliveryAddress$ = this.delivery.get(this.data.deliveryAddressId);
  }

  ticket() {
    console.log('Imprimir ticket');
  }

}

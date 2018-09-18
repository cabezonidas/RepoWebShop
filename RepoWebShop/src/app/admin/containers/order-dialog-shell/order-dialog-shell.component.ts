import { Component, OnInit, Inject, ViewChildren, ViewChild, ElementRef, EventEmitter, Output, OnDestroy } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { IOrder } from '../../interfaces/iorder';
import { Observable, fromEvent, Subscription } from 'rxjs';
import { DeliveryAddress } from '../../../cart/classes/delivery-address';
import { OrdersService } from '../../services/orders.service';
import { IAmountTitle } from '../../interfaces/iamount-title';
import { DeliveryService } from '../../services/delivery.service';
import { IDiscount } from '../../../cart/interfaces/idiscount';
import { switchMap, tap } from 'rxjs/operators';

@Component({
  selector: 'app-order-dialog-shell',
  templateUrl: './order-dialog-shell.component.html',
  styleUrls: ['./order-dialog-shell.component.scss']
})
export class OrderDialogShellComponent implements OnInit, OnDestroy {

  constructor(@Inject(MAT_DIALOG_DATA) public data: IOrder, private orders: OrdersService, private delivery: DeliveryService,
    public dialogRef: MatDialogRef<OrderDialogShellComponent>) {}

  printSub = new Subscription();
  deliveryAddress$: Observable<DeliveryAddress>;
  customerName$: Observable<string>;
  customerNumbers$: Observable<string>;
  customerEmails$: Observable<string>;
  discount$: Observable<IDiscount>;
  detailsBreakDown$: Observable<IAmountTitle[]>;

  loadingPrint = false;
  errorPrint = false;

  @ViewChild('printTicket') printTicket: ElementRef;


  ngOnInit() {
    this.customerName$ = this.orders.getCustomerName(this.data.orderId);
    this.customerNumbers$ = this.orders.getCustomerNumbers(this.data.orderId);
    this.customerEmails$ = this.orders.getCustomerEmails(this.data.orderId);
    this.detailsBreakDown$ = this.orders.getOrderBreakDown(this.data.orderId);
    this.discount$ = this.orders.getDiscount(this.data.discountId);
    this.deliveryAddress$ = this.delivery.get(this.data.deliveryAddressId);

    this.printSub = fromEvent(this.printTicket.nativeElement, 'click').pipe(
      tap(() => this.loadingPrint = true),
      switchMap(() => this.orders.printTicket(this.data.orderId))
    ).subscribe(() => this.dialogRef.close(), () => {
      this.loadingPrint = false;
      this.errorPrint = true;
    });
  }

  ngOnDestroy() {
    this.printSub.unsubscribe();
  }
}

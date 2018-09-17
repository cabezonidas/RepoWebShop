import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { IOrder } from '../../interfaces/iorder';
import { OrdersService } from '../../services/orders.service';
import { Subscription } from 'rxjs';
import { IInvoiceData } from '../../interfaces/iinvoice-data';
import { BillingService } from '../../services/billing.service';

@Component({
  selector: 'app-order-billing',
  templateUrl: './order-billing.component.html',
  styleUrls: ['./order-billing.component.scss']
})
export class OrderBillingComponent implements OnInit, OnDestroy {

  @Input() order: IOrder;
  billingSub = new Subscription();
  invoice: IInvoiceData;
  constructor(private orders: OrdersService, public billing: BillingService) { }

  ngOnInit() {
    this.billingSub = this.orders.getBilling(this.order.orderId).subscribe(invoice => {
      this.invoice = invoice;
    });
  }

  ngOnDestroy() {
    this.billingSub.unsubscribe();
  }

  isSuccess() {
    if (!this.invoice) {
      return true;
    }
    if (this.invoice.resultado !== 'A') {
      return false;
    }
    if (this.invoice.caes.length === 0) {
      return false;
    }
    this.invoice.caes.forEach(cae => {
      if (cae.resultado !== 'A') {
        return false;
      }
    });
    return true;
  }
}

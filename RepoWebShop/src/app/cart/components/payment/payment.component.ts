import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { PaymentService } from '../../services/payment.service';
import { Subscription, Observable } from 'rxjs';
import { ITotals } from '../../interfaces/itotals';
import { switchMap } from 'rxjs/operators';
import { ScrollService } from '../../../home/services/scroll.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss']
})
export class PaymentComponent implements OnInit, OnDestroy {

  paymentSub = new Subscription();
  @Input() total: Observable<number>;
  mercadoLink: string;
  constructor(private payment: PaymentService, private scroll: ScrollService) { }

  ngOnInit() {
    this.paymentSub = this.total.pipe(
      switchMap(() => this.payment.getMercadoPagoLink())
    ).subscribe(obj => {
      this.mercadoLink = obj.link;
    });
  }

  ngOnDestroy() {
    this.paymentSub.unsubscribe();
  }

  goPay = () => window.location.assign(this.mercadoLink);
}

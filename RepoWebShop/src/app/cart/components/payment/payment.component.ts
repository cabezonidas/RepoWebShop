import { Component, OnInit, OnDestroy, Input, ViewChild, ElementRef } from '@angular/core';
import { PaymentService } from '../../services/payment.service';
import { Subscription, Observable, fromEvent } from 'rxjs';
import { switchMap, tap } from 'rxjs/operators';
import { ScrollService } from '../../../home/services/scroll.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss']
})
export class PaymentComponent implements OnInit, OnDestroy {

  paymentSub = new Subscription();
  reloadSub = new Subscription();
  @Input() total: Observable<number>;
  @ViewChild('retry') retry: ElementRef;
  mercadoLink: string;
  loading = true;
  mercadoLinkFailed = false;
  retrying = false;
  constructor(private payment: PaymentService, private scroll: ScrollService) { }

  ngOnInit() {
    this.paymentSub = this.total.pipe(
      switchMap(() => this.payment.getMercadoPagoLink())
    ).subscribe(obj => {
      this.loading = false;
      if (obj.link) {
        this.mercadoLink = obj.link;
      } else {
        this.mercadoLinkFailed = true;
      }
    });

    this.reloadSub = fromEvent(this.retry.nativeElement, 'click').pipe(
      tap(() => this.retrying = true),
      switchMap(() => this.payment.getMercadoPagoLink())
    ).subscribe(obj => {
      this.retrying = false;
      if (obj.link) {
        this.mercadoLink = obj.link;
        this.mercadoLinkFailed = false;
      } else {
        this.mercadoLinkFailed = true;
      }
    });
  }


  ngOnDestroy() {
    this.paymentSub.unsubscribe();
    this.reloadSub.unsubscribe();
  }

  goPay = () => window.location.assign(this.mercadoLink);
}

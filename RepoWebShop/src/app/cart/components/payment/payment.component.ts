import { Component, OnInit, OnDestroy } from '@angular/core';
import { PaymentService } from '../../services/payment.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss']
})
export class PaymentComponent implements OnInit, OnDestroy {

  paymentSub = new Subscription();
  mercadoLink: string;
  constructor(private payment: PaymentService) { }

  ngOnInit() {
    this.paymentSub = this.payment.getMercadoPagoLink().subscribe(obj => {
      this.mercadoLink = obj.link;
    });
  }

  ngOnDestroy() {
    this.paymentSub.unsubscribe();
  }

  goPay = () => window.location.assign(this.mercadoLink);
}

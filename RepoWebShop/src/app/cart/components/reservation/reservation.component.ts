import { Component, OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import { PaymentService } from '../../services/payment.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.scss']
})
export class ReservationComponent implements OnInit, OnDestroy {
  bookingSub = new Subscription();
  loading = true;
  booked = false;
  constructor(private payment: PaymentService) { }

  ngOnInit() {
    this.bookingSub = this.payment.confirmReservaion().subscribe(
      (friendlyBookingId) => this.bookingSuccess(friendlyBookingId),
      (err) => this.bookingFail(err));
  }

  ngOnDestroy() {
    this.bookingSub.unsubscribe();
  }

  bookingSuccess = (friendlyBookingId: string) => {
    console.log('redirectUrl', friendlyBookingId);
    this.loading = false;
    this.booked = true;
    window.location.assign('/Order/Status/' + friendlyBookingId);
  }

  bookingFail = (err: string) => {
    console.log(err);
    this.loading = false;
    this.booked = false;
  }
}

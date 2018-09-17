import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { DeliveryService } from '../../services/delivery.service';
import { DeliveryAddress } from '../../../cart/classes/delivery-address';

@Component({
  selector: 'app-order-delivery',
  templateUrl: './order-delivery.component.html',
  styleUrls: ['./order-delivery.component.scss']
})
export class OrderDeliveryComponent implements OnInit, OnDestroy {

  @Input() deliveryId: number;

  deliverySub = new Subscription();
  address: DeliveryAddress;

  constructor(private delivery: DeliveryService) { }

  ngOnInit() {
    if (this.deliveryId) {
      this.delivery.get(this.deliveryId).subscribe(address => {
        if (address) {
          this.address = address;
        }
      });
    }
  }

  ngOnDestroy() {
    this.deliverySub.unsubscribe();
  }

}

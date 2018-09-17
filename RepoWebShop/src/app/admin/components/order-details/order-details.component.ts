import { Component, OnInit, Input, OnChanges, OnDestroy, EventEmitter, Output } from '@angular/core';
import { IOrderItem } from '../../interfaces/iorder-item';
import { IOrderCatering } from '../../interfaces/iorder-catering';
import { IOrderPie } from '../../interfaces/iorder-pie';
import { OrdersService } from '../../services/orders.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.scss']
})
export class OrderDetailsComponent implements OnInit, OnChanges, OnDestroy {

  @Input() orderId: number;
  @Input() orderReady: boolean;
  @Input() orderPickedUp: boolean;

  @Output() save = new EventEmitter();
  @Output() notify = new EventEmitter();

  itemSub = new Subscription();
  cateSub = new Subscription();
  piesSub = new Subscription();
  hideNoty = false;
  hideArch = false;

  caterings: IOrderCatering[];
  items: IOrderItem[];
  pies: IOrderPie[];
  constructor(private orderService: OrdersService) { }

  ngOnInit() {
    this.itemSub = this.orderService.getOrderItems(this.orderId).subscribe(items => this.items = items);
    this.cateSub = this.orderService.getCaterings(this.orderId).subscribe(caterings => this.caterings = caterings);
    this.piesSub = this.orderService.getOrderPies(this.orderId).subscribe(pies => this.pies = pies);
  }

  ngOnChanges() {
  }

  ngOnDestroy() {
    this.itemSub.unsubscribe();
    this.cateSub.unsubscribe();
    this.piesSub.unsubscribe();
  }

  saveAndHide = () => {
    this.hideArch = true;
    this.save.emit();
  }
  notifyAndHide = () => {
    this.hideNoty = true;
    this.notify.emit();
  }

}

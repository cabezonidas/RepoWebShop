import { Component, OnInit, Input, OnChanges, Output, EventEmitter } from '@angular/core';
import { IPickupOption } from '../../interfaces/ipickup-option';
import { IPickupDate } from '../../../../interfaces/ipickup-date';
import { CalendarService } from '../../../../../home/services/calendar.service';
import { DeliveryAddress } from '../../../../classes/delivery-address';

@Component({
  selector: 'app-pickup-options',
  templateUrl: './pickup-options.component.html',
  styleUrls: ['./pickup-options.component.scss']
})
export class PickupOptionsComponent implements OnInit, OnChanges {

  @Input() options: IPickupOption[];
  @Input() pickup: IPickupDate;
  @Input() totalWithoutDiscount: number;
  @Input() total: number;
  @Input() loading: boolean;
  @Input() deliveryAddress: DeliveryAddress;

  optionsCount: number;
  hasDiscount: boolean;

  @Output() selectPickup = new EventEmitter<string>();

  constructor(private calendar: CalendarService) { }

  ngOnInit() {

  }

  ngOnChanges() {

    this.optionsCount = this.countOptions();
    this.hasDiscount = this.total < this.totalWithoutDiscount;
  }

  countOptions = (): number => {
    let count = 0;
    if (this.options) {
      this.options.forEach(x => {
        if (x.dayOptions) { x.dayOptions.forEach(() => { count += 1; }); }
      });
    }
    return count;
  }

  readyFor = (): string => this.calendar.soonestPickup(new Date(this.pickup.from));
}

import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IOrder } from '../../interfaces/iorder';
import { CalendarService } from '../../../home/services/calendar.service';

@Component({
  selector: 'app-order-summary',
  templateUrl: './order-summary.component.html',
  styleUrls: ['./order-summary.component.scss']
})
export class OrderSummaryComponent implements OnInit {
  @Input() order: IOrder;
  @Output() openDialog = new EventEmitter();

  constructor(public calendar: CalendarService) { }

  ngOnInit() {
  }

}

import { Component, OnInit, Input, OnDestroy, OnChanges } from '@angular/core';
import { ProductsService } from '../../services/products.service';
import { Subscriber, Subscription } from 'rxjs';
import { CalendarService } from '../../../home/services/calendar.service';

@Component({
  selector: 'app-soonest-pickup',
  templateUrl: './soonest-pickup.component.html',
  styleUrls: ['./soonest-pickup.component.scss']
})
export class SoonestPickupComponent implements OnInit {

  @Input() soonestPickup: string;
  constructor(private calendar: CalendarService) { }

  ngOnInit() {
  }
}

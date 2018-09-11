import { Component, OnInit, Input, OnDestroy, OnChanges, EventEmitter, Output } from '@angular/core';
import { ProductsService } from '../../services/products.service';
import { Subscriber, Subscription } from 'rxjs';
import { CalendarService } from '../../../home/services/calendar.service';

@Component({
  selector: 'app-soonest-pickup',
  templateUrl: './soonest-pickup.component.html',
  styleUrls: ['./soonest-pickup.component.scss']
})
export class SoonestPickupComponent implements OnInit, OnDestroy {

  timeSub = new Subscription();
  @Input() soonestPickup: number;
  @Output() dateLoaded = new EventEmitter();
  displayPickup: string;

  constructor(private calendar: CalendarService) { }

  ngOnInit() {
    this.timeSub = this.calendar.getPickup(this.soonestPickup).subscribe(date => {
      this.displayPickup = this.calendar.soonestPickup(new Date(date));
      this.dateLoaded.emit();
    });
  }

  ngOnDestroy() {
    this.timeSub.unsubscribe();
  }
}

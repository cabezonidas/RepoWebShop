import { Component, OnInit, Input, OnChanges, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { CalendarService } from '../../../home/services/calendar.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-soonest-pickup',
  templateUrl: './soonest-pickup.component.html',
  styleUrls: ['./soonest-pickup.component.scss']
})
export class SoonestPickupComponent implements OnInit, OnChanges {

  @Input() soonestPickup: number;
  @Output() dateLoaded = new EventEmitter();
  displayPickup: string;
  displayPickup$: Observable<string>;

  constructor(private calendar: CalendarService) { }

  ngOnInit() { }
  ngOnChanges() {
    this.displayPickup$ = this.calendar.getPickup(this.soonestPickup).pipe(
      map(date => this.calendar.soonestPickup(new Date(date)))
    );
  }
}

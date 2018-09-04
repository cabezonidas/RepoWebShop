import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ICatering } from '../../interfaces/ICatering';
import { CalendarService } from '../../../home/services/calendar.service';

@Component({
  selector: 'app-catering-option',
  templateUrl: './catering-option.component.html',
  styleUrls: ['./catering-option.component.scss']
})
export class CateringOptionComponent implements OnInit {

  @Input() catering: ICatering;
  @Output() addCatering = new EventEmitter<number>();
  @Output() copyCatering = new EventEmitter<number>();
  constructor(private calendar: CalendarService) { }

  ngOnInit() {
  }
  soonestPickup = (date: Date) => this.calendar.soonestPickup(new Date(date));
}

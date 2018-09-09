import { Component, OnInit, Input, Output, EventEmitter, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { ICatering } from '../../interfaces/ICatering';
import { CalendarService } from '../../../home/services/calendar.service';

@Component({
  selector: 'app-catering-option',
  templateUrl: './catering-option.component.html',
  styleUrls: ['./catering-option.component.scss']
})
export class CateringOptionComponent implements OnInit, AfterViewInit {

  cardWidth = 350;
  @Input() catering: ICatering;
  @Output() addCatering = new EventEmitter<number>();
  @Output() copyCatering = new EventEmitter<number>();
  @Output() next = new EventEmitter();
  @Output() prev = new EventEmitter();

  constructor(private calendar: CalendarService) { }

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.onResize();
  }

  onResize() {
    this.cardWidth = window.innerWidth > 370 ? 350 : window.innerWidth - 20;
  }

  soonestPickup = (date: Date) => this.calendar.soonestPickup(new Date(date));
}

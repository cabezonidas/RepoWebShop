import { Component, OnInit, OnDestroy } from '@angular/core';
import { CalendarService } from '../../services/calendar.service';
import { Subscription } from 'rxjs';
import { IPublicCalendar } from '../../interfaces/ipublic-calendar';
import { map, tap, groupBy, switchMap, toArray, mergeMap } from 'rxjs/operators';
import { IWorkingHour } from '../../interfaces/iworking-hour';
import { IPublicHoliday } from '../../interfaces/ipublic-holiday';
import { IVacation } from '../../interfaces/ivacation';

@Component({
  selector: 'app-hours',
  templateUrl: './hours.component.html',
  styleUrls: ['./hours.component.scss']
})
export class HoursComponent implements OnInit, OnDestroy {
  workingHoursSub = new Subscription;
  publicHolidaysSub = new Subscription;
  vacationsSub = new Subscription;
  publicCalendar: IPublicCalendar;

  publicCalendarArray: Array<IWorkingHour[]>;
  publicHolidayArray: Array<IPublicHoliday[]>;
  vacations: IVacation[];

  constructor(private calendar: CalendarService) { }

  ngOnInit() {
    this.publicCalendarArray = [];
    this.publicHolidayArray = [];
    this.vacations = [];

    this.workingHoursSub = this.calendar.getPublicCalendar().pipe(
      switchMap(publicCalendar => publicCalendar.openHours.sort((a, b) => a.dayId - b.dayId)),
      groupBy(openHour => openHour.dayId),
      mergeMap(group => group.pipe(toArray())),
      map(wh => wh.sort((a, b) => ('' + a.startingAt).localeCompare(b.startingAt)))
    ).subscribe(whByDay => {
      this.publicCalendarArray.push(whByDay);
    });

    this.publicHolidaysSub = this.calendar.getPublicCalendar().pipe(
      switchMap(publicCalendar => publicCalendar.publicHolidays.sort((a, b) => b.date.valueOf() - a.date.valueOf())),
      groupBy(publicHoliday => publicHoliday.date),
      mergeMap(group => group.pipe(toArray())),
      map(wh => wh.sort((a, b) => b.date.valueOf() - a.date.valueOf()))
    ).subscribe(publicHoliday => {
      this.publicHolidayArray.push(publicHoliday);
    });

    this.vacationsSub = this.calendar.getPublicCalendar().pipe(
      switchMap(publicCalendar => publicCalendar.vacations.sort((a, b) => ('' + a.startDate).localeCompare(b.startDate))),
    ).subscribe(vacation => this.vacations.push(vacation));
  }

  ngOnDestroy() {
    this.workingHoursSub.unsubscribe();
    this.publicHolidaysSub.unsubscribe();
    this.vacationsSub.unsubscribe();
  }

  day = (i: number): string => this.calendar.day(i);
  spDay = (date: Date): string => this.calendar.spDay(new Date(date));
  starting = (hours: IWorkingHour): Date => this.calendar.starting(hours);
  closing = (hours: IWorkingHour): Date => this.calendar.closing(hours);
}

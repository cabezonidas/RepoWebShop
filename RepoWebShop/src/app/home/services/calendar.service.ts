import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPublicCalendar } from '../interfaces/ipublic-calendar';
import { IWorkingHour } from '../interfaces/iworking-hour';
import { IPublicHoliday } from '../interfaces/ipublic-holiday';

@Injectable({
  providedIn: 'root'
})
export class CalendarService {

  constructor(private http: HttpClient) { }

  getPublicCalendar = (): Observable<IPublicCalendar> => this.http.get<IPublicCalendar>('/api/_calendar/publicCalendar');

  day(i: number) {
    switch (i) {
      case 0: return 'Domingo';
      case 1: return 'Lunes';
      case 2: return 'Martes';
      case 3: return 'Miércoles';
      case 4: return 'Jueves';
      case 5: return 'Viernes';
      case 6: return 'Sábado';
      default: return 'Fecha inválida';
    }
  }

  month(i: number) {
    switch (i) {
      case 0: return 'Enero';
      case 1: return 'Febrero';
      case 2: return 'Marzo';
      case 3: return 'Abril';
      case 4: return 'Mayo';
      case 5: return 'Junio';
      case 6: return 'Julio';
      case 7: return 'Agosto';
      case 8: return 'Septiembre';
      case 9: return 'Octubre';
      case 10: return 'Noviembre';
      case 11: return 'Diciembre';
      default: return 'Fecha inválida';
    }
  }

  toDateWithOutTimeZone(date) {
    const tempTime = date.split(':');
    const dt = new Date();
    dt.setHours(tempTime[0]);
    dt.setMinutes(tempTime[1]);
    dt.setSeconds(tempTime[2]);
    return dt;
  }

  starting = (hours: IWorkingHour): Date => this.toDateWithOutTimeZone(hours.startingAt);
  closing = (hours: IWorkingHour): Date => {
    const start = this.toDateWithOutTimeZone(hours.startingAt);
    const duration = this.toDateWithOutTimeZone(hours.duration);
    const addMinutes = duration.getMinutes() + (duration.getHours() * 60);
    return new Date(start.getTime() + (addMinutes * 60000));
  }
  spDay = (date: Date): string => {
    return `${this.day((date.getDay()))} ${date.getDate()} de ${this.month(date.getMonth())}`;
  }

  soonestPickup = (date: Date): string => {
    return `el ${this.day((date.getDay()))} ${date.getDate()} a las ${date.getHours()}:${date.getMinutes()}`;
  }
}

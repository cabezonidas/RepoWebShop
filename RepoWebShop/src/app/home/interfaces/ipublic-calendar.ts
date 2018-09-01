import { IWorkingHour } from './iworking-hour';
import { IPublicHoliday } from './ipublic-holiday';
import { IVacation } from './ivacation';

export interface IPublicCalendar {
    'openHours': IWorkingHour[];
    'publicHolidays': IPublicHoliday[];
    'vacations': IVacation[];
}

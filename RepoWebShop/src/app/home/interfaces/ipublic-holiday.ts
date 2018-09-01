import { IWorkingHour } from './iworking-hour';

export interface IPublicHoliday {
    'publicHolidayId': number;
    'date': Date;
    'openHours': IWorkingHour[];
    'processingHours': IWorkingHour[];
}

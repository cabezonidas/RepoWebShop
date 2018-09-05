import { IDayOption } from './iday-option';

export interface IPickupOption {
    day: string;
    date: Date;
    dayOptions: IDayOption[];
}

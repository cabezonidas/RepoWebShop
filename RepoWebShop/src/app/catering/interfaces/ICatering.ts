import { ICateringItem } from './ICateringItem';
import { ICateringMiscellaneous } from './ICateringMiscellaneous';

export interface ICatering {
    'lunchId': number;
    'items': ICateringItem[];
    'miscellanea': ICateringMiscellaneous[];
    'comments': string;
    'isCombo': boolean;
    'isGeneric': boolean;
    'title': string;
    'description': string;
    'thumbnailUrl': string;
    'preparationTime': string;
    'eventDuration': string;
    'attendants': string;
}

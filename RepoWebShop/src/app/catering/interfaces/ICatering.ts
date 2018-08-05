import { ICateringItem } from './ICateringItem';
import { ICateringMiscellaneous } from './ICateringMiscellaneous';

export interface ICatering {
    'items': ICateringItem[];
    'miscellanea': ICateringMiscellaneous[];
    'preparationTime': string;
}

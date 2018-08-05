import { IItem } from '../../products/interfaces/iitem';

export interface ICateringItem {
    'item': IItem;
    'quantity': number;
    'itemCount': number;
    'subTotal': number;
    'subTotalInStore': number;
}

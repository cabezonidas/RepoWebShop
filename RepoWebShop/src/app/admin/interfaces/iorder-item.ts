import { IItem } from '../../products/interfaces/iitem';

export interface IOrderItem {
    amount: number;
    item: IItem;
}

import { IPieDetail } from './ipie-detail';
import { IItem } from './iitem';

export interface IProduct {
    pieDetail: IPieDetail;
    primaryPicture: string;
    items: Array<IItem>;
}

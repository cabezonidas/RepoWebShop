import { IItem } from './iitem';

export interface IProduct {
    pieDetailId: number;
    name: string;
    shortDescription: string;
    longDescription: string;
    isActive: boolean;
    isPieOfTheWeek: boolean;
    ingredients: string;
    flickrAlbumId: string;
    primaryPicture: string;
    items: Array<IItem>;
}

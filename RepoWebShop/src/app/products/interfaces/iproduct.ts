import { IItem } from './iitem';
import { IAlbum } from './ialbum';

export interface IProduct {
    pieDetailId: number;
    name: string;
    shortDescription: string;
    longDescription: string;
    isActive: boolean;
    isPieOfTheWeek: boolean;
    ingredients: string;
    flickrAlbumId: string;
    album: IAlbum;
    primaryPicture: string;
    items: Array<IItem>;
}

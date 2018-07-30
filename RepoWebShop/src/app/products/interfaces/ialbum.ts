import { IPhoto } from './iphoto';

export interface IAlbum {
    albumId: string;
    primaryPicture: string;
    title: string;
    photos: Array<IPhoto>;
}

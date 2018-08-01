import { Action } from '@ngrx/store';
import { IProduct } from '../interfaces/iproduct';
import { IAlbum } from '../interfaces/ialbum';

export enum ProductActionTypes {
    LoadProducts = '[Product] Load',
    LoadProductsSuccess = '[Product] Load Success',
    LoadProductsFail = '[Product] Load Fail',
    LoadAlbum = '[Album] Load',
    LoadAlbumSuccess = '[Album] Load Success',
    LoadAlbumFail = '[Album] Load Fail'
}

export class LoadProducts implements Action {
    readonly type = ProductActionTypes.LoadProducts;
}

export class LoadProductsSuccess implements Action {
    readonly type = ProductActionTypes.LoadProductsSuccess;
    constructor(public payload: IProduct[]) {}
}

export class LoadProductsFail implements Action {
    readonly type = ProductActionTypes.LoadProductsFail;
    constructor(public payload: string) {}
}

export class LoadAlbum implements Action {
    readonly type = ProductActionTypes.LoadAlbum;
    constructor(public payload: string) {}
}

export class LoadAlbumSuccess implements Action {
    readonly type = ProductActionTypes.LoadAlbumSuccess;
    constructor(public payload: IAlbum) {}
}

export class LoadAlbumFail implements Action {
    readonly type = ProductActionTypes.LoadAlbumFail;
    constructor(public payload: string) {}
}

export type ProductActions = LoadProducts
    | LoadProductsSuccess
    | LoadProductsFail
    | LoadAlbum
    | LoadAlbumSuccess
    | LoadAlbumFail;

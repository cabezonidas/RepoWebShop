import { Action } from '@ngrx/store';
import { IProduct } from '../interfaces/iproduct';

export enum ProductActionTypes {
    Load = '[Product] Load',
    LoadSuccess = '[Product] Load Success',
    LoadFail = '[Product] Load Fail'
}

export class Load implements Action {
    readonly type = ProductActionTypes.Load;
}

export class LoadSuccess implements Action {
    readonly type = ProductActionTypes.LoadSuccess;
    constructor(public payload: IProduct[]) {}
}

export class LoadFail implements Action {
    readonly type = ProductActionTypes.LoadFail;
    constructor(public payload: string) {}
}

export type ProductActions = Load
    | LoadSuccess
    | LoadFail;

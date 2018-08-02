import { Action } from '@ngrx/store';
import { IItem } from '../../products/interfaces/iitem';

export enum CateringActionTypes {
    LoadItems = '[Catering] Load',
    LoadItemsSuccess = '[Catering] Load Success',
    LoadItemsFail = '[Catering] Load Fail'
}

export class LoadItems implements Action {
    readonly type = CateringActionTypes.LoadItems;
}

export class LoadItemsSuccess implements Action {
    readonly type = CateringActionTypes.LoadItemsSuccess;
    constructor(public payload: IItem[]) {}
}

export class LoadItemsFail implements Action {
    readonly type = CateringActionTypes.LoadItemsFail;
    constructor(public payload: string) {}
}

export type CateringActions = LoadItems
    | LoadItemsSuccess
    | LoadItemsFail;

import { Action } from '@ngrx/store';
import { IItem } from '../../products/interfaces/iitem';
import { ICatering } from '../interfaces/ICatering';

export enum CateringActionTypes {
    AddItem = '[Catering] Add Item to Catering',
    RemoveItem = '[Catering] Remove Item from Catering',
    LoadCaterings = '[Catering] Load Caterings',
    LoadCateringsSuccess = '[Catering] Load Caterings Success',
    LoadCateringsFail = '[Catering] Load Caterings Fail',
    LoadSessionCateringDone = '[Catering] Session Catering Loaded [Success/Fail]'
}

export class LoadSessionCateringDone implements Action {
    readonly type = CateringActionTypes.LoadSessionCateringDone;
    constructor() {}
}

export class LoadCaterings implements Action {
    readonly type = CateringActionTypes.LoadCaterings;
}

export class LoadCateringsSuccess implements Action {
    readonly type = CateringActionTypes.LoadCateringsSuccess;
    constructor(public payload: ICatering[]) {}
}

export class LoadCateringsFail implements Action {
    readonly type = CateringActionTypes.LoadCateringsFail;
    constructor(public payload: string) {}
}

export class AddItem implements Action {
    readonly type = CateringActionTypes.AddItem;
    constructor(public payload: number) {}
}

export class RemoveItem implements Action {
    readonly type = CateringActionTypes.RemoveItem;
    constructor(public payload: number) {}
}

export type CateringActions = LoadSessionCateringDone
    | AddItem
    | RemoveItem
    | LoadCaterings
    | LoadCateringsSuccess
    | LoadCateringsFail;

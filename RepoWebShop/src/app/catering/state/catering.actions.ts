import { Action } from '@ngrx/store';
import { IItem } from '../../products/interfaces/iitem';
import { ICatering } from '../interfaces/ICatering';

export enum CateringActionTypes {
    LoadItems = '[Catering] Load Items',
    LoadItemsSuccess = '[Catering] Load Items Success',
    LoadItemsFail = '[Catering] Load Items Fail',
    LoadSessionCatering = '[Catering] Load Session Catering',
    LoadSessionCateringSuccess = '[Catering] Load Session Catering Success',
    LoadSessionCateringFail = '[Catering] Load Session Catering Fail',
    AddItem = '[Catering] Add Item to Catering',
    RemoveItem = '[Catering] Remove Item from Catering',
    LoadCaterings = '[Catering] Load Caterings',
    LoadCateringsSuccess = '[Catering] Load Caterings Success',
    LoadCateringsFail = '[Catering] Load Caterings Fail'
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

export class LoadSessionCatering implements Action {
    readonly type = CateringActionTypes.LoadSessionCatering;
}

export class LoadSessionCateringSuccess implements Action {
    readonly type = CateringActionTypes.LoadSessionCateringSuccess;
    constructor(public payload: ICatering) {}
}

export class LoadSessionCateringFail implements Action {
    readonly type = CateringActionTypes.LoadSessionCateringFail;
    constructor(public payload: string) {}
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

export type CateringActions = LoadItems
    | LoadItemsSuccess
    | LoadItemsFail
    | LoadSessionCatering
    | LoadSessionCateringSuccess
    | LoadSessionCateringFail
    | AddItem
    | RemoveItem
    | LoadCaterings
    | LoadCateringsSuccess
    | LoadCateringsFail;

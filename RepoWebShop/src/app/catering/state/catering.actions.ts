import { Action } from '@ngrx/store';
import { IItem } from '../../products/interfaces/iitem';
import { ICatering } from '../interfaces/ICatering';
import { ICateringItem } from '../interfaces/ICateringItem';

export enum CateringActionTypes {
    AddItem = '[Catering] Add Item to Catering',
    RemoveItem = '[Catering] Remove Item from Catering',
    AddLocalItem = '[Catering] Add Local Item to Catering',
    RemoveLocalItem = '[Catering] Remove Local Item from Catering',
    LoadCaterings = '[Catering] Load Caterings',
    LoadCateringsSuccess = '[Catering] Load Caterings Success',
    LoadCateringsFail = '[Catering] Load Caterings Fail',
    LoadCustomCatering = '[Catering] Load Custom Catering',
    LoadCustomCateringSuccess = '[Catering] Load Custom Catering Success',
    LoadCustomCateringFail = '[Catering] Load Custom Catering Fail',
    LoadSessionCateringDone = '[Catering] Session Catering Loaded [Success/Fail]',
    SavingCustomCatering = '[Catering] Saving Custom Catering',
    SavingCustomCateringSuccess = '[Catering] Saving Custom Catering Success',
    SavingCustomCateringFail = '[Catering] Saving Custom Catering Fail',
}

export class LoadSessionCateringDone implements Action {
    readonly type = CateringActionTypes.LoadSessionCateringDone;
    constructor() {}
}

export class SavingCustomCatering implements Action {
    readonly type = CateringActionTypes.SavingCustomCatering;
    constructor(public payload: ICateringItem[]) {}
}

export class SavingCustomCateringSuccess implements Action {
    readonly type = CateringActionTypes.SavingCustomCateringSuccess;
    constructor(public payload: ICatering) {}
}

export class SavingCustomCateringFail implements Action {
    readonly type = CateringActionTypes.SavingCustomCateringFail;
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

export class LoadCustomCatering implements Action {
    readonly type = CateringActionTypes.LoadCustomCatering;
}

export class LoadCustomCateringSuccess implements Action {
    readonly type = CateringActionTypes.LoadCustomCateringSuccess;
    constructor(public payload: ICatering) {}
}

export class LoadCustomCateringFail implements Action {
    readonly type = CateringActionTypes.LoadCustomCateringFail;
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

export class AddLocalItem implements Action {
    readonly type = CateringActionTypes.AddLocalItem;
    constructor(public payload: IItem) {}
}

export class RemoveLocalItem implements Action {
    readonly type = CateringActionTypes.RemoveLocalItem;
    constructor(public payload: IItem) {}
}

export type CateringActions = LoadSessionCateringDone
    | AddItem
    | RemoveItem
    | AddLocalItem
    | RemoveLocalItem
    | LoadCaterings
    | LoadCateringsSuccess
    | LoadCateringsFail
    | LoadCustomCatering
    | LoadCustomCateringSuccess
    | LoadCustomCateringFail
    | SavingCustomCatering
    | SavingCustomCateringSuccess
    | SavingCustomCateringFail;

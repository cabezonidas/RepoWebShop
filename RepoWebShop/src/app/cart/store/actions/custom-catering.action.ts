import { Action } from '@ngrx/store';
import { ICatering } from '../../../catering/interfaces/ICatering';

export enum CustomCateringActionTypes {
    LoadSessionCatering = '[Session Catering] Load Session Catering',
    LoadSessionCateringSuccess = '[Session Catering] Load Session Catering success',
    LoadSessionCateringFail = '[Session Catering] Load Session Catering fail',

    RemoveSessionCatering = '[Session Catering] Remove Session Catering',
    RemoveSessionCateringSuccess = '[Session Catering] Remove Session Catering success',
    RemoveSessionCateringFail = '[Session Catering] Remove Session Catering fail',
}

export class LoadSessionCatering implements Action {
    readonly type = CustomCateringActionTypes.LoadSessionCatering;
    constructor() {}
}

export class LoadSessionCateringSuccess implements Action {
    readonly type = CustomCateringActionTypes.LoadSessionCateringSuccess;
    constructor(public payload: ICatering) {}
}

export class LoadSessionCateringFail implements Action {
    readonly type = CustomCateringActionTypes.LoadSessionCateringFail;
    constructor(public payload: any) {}
}

export class RemoveSessionCatering implements Action {
    readonly type = CustomCateringActionTypes.RemoveSessionCatering;
    constructor() {}
}

export class RemoveSessionCateringSuccess implements Action {
    readonly type = CustomCateringActionTypes.RemoveSessionCateringSuccess;
    constructor() {}
}

export class RemoveSessionCateringFail implements Action {
    readonly type = CustomCateringActionTypes.RemoveSessionCateringFail;
    constructor(public payload: any) {}
}

export type CustomCateringActions = LoadSessionCatering
    | LoadSessionCateringSuccess
    | LoadSessionCateringFail
    | RemoveSessionCatering
    | RemoveSessionCateringSuccess
    | RemoveSessionCateringFail
import { Action } from '@ngrx/store';
import { ICartCatering } from '../../interfaces/icart-catering';

export enum CateringActionTypes {
    AddCatering = '[Cart] Add catering',
    AddCateringSuccess = '[Cart] Add catering success',
    AddCateringFail = '[Cart] Add catering fail',

    RemoveCatering = '[Cart] Remove catering',
    RemoveCateringSuccess = '[Cart] Remove catering success',
    RemoveCateringFail = '[Cart] Remove catering fail',

    LoadCaterings = '[Cart] Load caterings',
    LoadCateringsSuccess = '[Cart] Load caterings success',
    LoadCateringsFail = '[Cart] Load caterings fail'
}

export class AddCatering implements Action {
    readonly type = CateringActionTypes.AddCatering;
    constructor(public payload: number) {}
}

export class AddCateringFail implements Action {
    readonly type = CateringActionTypes.AddCateringFail;
    constructor(public payload: any) {}
}

export class AddCateringSuccess implements Action {
  readonly type = CateringActionTypes.AddCateringSuccess;
  constructor(public payload: ICartCatering[]) {}
}

export class RemoveCatering implements Action {
    readonly type = CateringActionTypes.RemoveCatering;
    constructor(public payload: number) {}
}

export class RemoveCateringFail implements Action {
    readonly type = CateringActionTypes.RemoveCateringFail;
    constructor(public payload: any) {}
}

export class RemoveCateringSuccess implements Action {
  readonly type = CateringActionTypes.RemoveCateringSuccess;
  constructor(public payload: ICartCatering[]) {}
}

export class LoadCaterings implements Action {
    readonly type = CateringActionTypes.LoadCaterings;
    constructor() {}
}

export class LoadCateringsFail implements Action {
    readonly type = CateringActionTypes.LoadCateringsFail;
    constructor(public payload: any) {}
}

export class LoadCateringsSuccess implements Action {
  readonly type = CateringActionTypes.LoadCateringsSuccess;
  constructor(public payload: ICartCatering[]) {}
}

export type CateringActions =
  | AddCatering
  | AddCateringFail
  | AddCateringSuccess
  | RemoveCatering
  | RemoveCateringFail
  | RemoveCateringSuccess
  | LoadCaterings
  | LoadCateringsFail
  | LoadCateringsSuccess;

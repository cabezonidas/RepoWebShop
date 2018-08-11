import { Action } from '@ngrx/store';
import { ICartCatering } from '../../interfaces/icart-catering';

export enum CateringActionTypes {
    Add = '[Cart] Add catering',
    AddSuccess = '[Cart] Add catering success',
    AddFail = '[Cart] Add catering fail',
    
    Remove = '[Cart] Remove catering',
    RemoveSuccess = '[Cart] Remove catering success',
    RemoveFail = '[Cart] Remove catering fail',

    Load = '[Cart] Load caterings',
    LoadSuccess = '[Cart] Load caterings success',
    LoadFail = '[Cart] Load caterings fail'
}

export class AddCatering implements Action {
    readonly type = CateringActionTypes.Add;
    constructor(public payload: number) {}
}

export class AddCateringFail implements Action {
    readonly type = CateringActionTypes.AddFail;
    constructor(public payload: any) {}
}

export class AddCateringSuccess implements Action {
  readonly type = CateringActionTypes.AddSuccess;
  constructor(public payload: ICartCatering[]) {}
}

export class RemoveCatering implements Action {
    readonly type = CateringActionTypes.Remove;
    constructor(public payload: number) {}
}

export class RemoveCateringFail implements Action {
    readonly type = CateringActionTypes.RemoveFail;
    constructor(public payload: any) {}
}

export class RemoveCateringSuccess implements Action {
  readonly type = CateringActionTypes.RemoveSuccess;
  constructor(public payload: ICartCatering[]) {}
}

export class LoadCaterings implements Action {
    readonly type = CateringActionTypes.Load;
}

export class LoadCateringsFail implements Action {
    readonly type = CateringActionTypes.LoadFail;
    constructor(public payload: any) {}
}

export class LoadCateringsSuccess implements Action {
  readonly type = CateringActionTypes.LoadSuccess;
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
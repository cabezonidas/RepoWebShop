import { Action } from '@ngrx/store';
import { IPickupOption } from '../../components/pickup/interfaces/ipickup-option';
import { IPickupDate } from '../../interfaces/ipickup-date';

export enum PickupActionTypes {
    LoadPickupOptions = '[Pickup] Load Pickup',
    LoadPickupOptionsFail = '[Pickup] Load Pickup fail',
    LoadPickupOptionsSuccess = '[Pickup] Load Pickup success',
    
    SetPickupOption = '[Pickup] Set Pickup Option',
    SetPickupOptionFail = '[Pickup] Set Pickup Option fail',
    SetPickupOptionSuccess = '[Pickup] Set Pickup Option success',

    GetPickupOption = '[Pickup] Get Pickup Option',
    GetPickupOptionFail = '[Pickup] Get Pickup Option fail',
    GetPickupOptionSuccess = '[Pickup] Get Pickup Option success',

    GetPreparationTime = '[Pickup] Get Preparation Time',
    GetPreparationTimeFail = '[Pickup] Get Preparation Time fail',
    GetPreparationTimeSuccess = '[Pickup] Get Preparation Time success',
}

export class LoadPickupOptions implements Action {
    readonly type = PickupActionTypes.LoadPickupOptions;
}

export class LoadPickupOptionsFail implements Action {
    readonly type = PickupActionTypes.LoadPickupOptionsFail;
    constructor(public payload: any) {}
}

export class LoadPickupOptionsSuccess implements Action {
  readonly type = PickupActionTypes.LoadPickupOptionsSuccess;
  constructor(public payload: IPickupOption[]) {}
}

export class SetPickupOption implements Action {
    readonly type = PickupActionTypes.SetPickupOption;
    constructor(public payload: string) {}
}

export class SetPickupOptionFail implements Action {
    readonly type = PickupActionTypes.SetPickupOptionFail;
    constructor(public payload: any) {}
}

export class SetPickupOptionSuccess implements Action {
  readonly type = PickupActionTypes.SetPickupOptionSuccess;
  constructor(public payload: IPickupDate) {}
}

export class GetPickupOption implements Action {
    readonly type = PickupActionTypes.GetPickupOption;
}

export class GetPickupOptionFail implements Action {
    readonly type = PickupActionTypes.GetPickupOptionFail;
    constructor(public payload: any) {}
}

export class GetPickupOptionSuccess implements Action {
  readonly type = PickupActionTypes.GetPickupOptionSuccess;
  constructor(public payload: IPickupDate) {}
}

export class GetPreparationTime implements Action {
    readonly type = PickupActionTypes.GetPreparationTime;
}

export class GetPreparationTimeFail implements Action {
    readonly type = PickupActionTypes.GetPreparationTimeFail;
    constructor(public payload: any) {}
}

export class GetPreparationTimeSuccess implements Action {
  readonly type = PickupActionTypes.GetPreparationTimeSuccess;
  constructor(public payload: number) {}
}

export type PickupActions =
  | LoadPickupOptions
  | LoadPickupOptionsFail
  | LoadPickupOptionsSuccess
  | SetPickupOption
  | SetPickupOptionFail
  | SetPickupOptionSuccess
  | GetPickupOption
  | GetPickupOptionFail
  | GetPickupOptionSuccess
  | GetPreparationTime
  | GetPreparationTimeFail
  | GetPreparationTimeSuccess;
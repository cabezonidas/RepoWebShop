import { Action } from '@ngrx/store';
import { IDiscount } from '../../interfaces/idiscount';

export enum DiscountActionTypes {
    Get = '[Discount] Add discount',
    GetFail = '[Discount] Add discount fail',
    GetSuccess = '[Discount] Add discount success',

    Apply= '[Discount] Apply discount',
    ApplyFail = '[Discount] Apply discount fail',
    ApplySuccess = '[Discount] Apply discount success'
}

export class Get implements Action {
    readonly type = DiscountActionTypes.Get;
    constructor() {}
}

export class GetFail implements Action {
    readonly type = DiscountActionTypes.GetFail;
    constructor(public payload: any) {}
}

export class GetSuccess implements Action {
  readonly type = DiscountActionTypes.GetSuccess;
  constructor(public payload: IDiscount) {}
}

export class Apply implements Action {
    readonly type = DiscountActionTypes.Apply;
    constructor(public payload: string) {}
}

export class ApplyFail implements Action {
    readonly type = DiscountActionTypes.ApplyFail;
    constructor(public payload: any) {}
}

export class ApplySuccess implements Action {
  readonly type = DiscountActionTypes.ApplySuccess;
  constructor(public payload: IDiscount) {}
}

export type DiscountActions = Get
  | GetFail
  | GetSuccess
  | Apply
  | ApplyFail
  | ApplySuccess;
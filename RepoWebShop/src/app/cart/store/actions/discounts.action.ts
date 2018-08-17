import { Action } from '@ngrx/store';
import { IDiscount } from '../../interfaces/idiscount';

export enum DiscountActionTypes {
    GetDiscount = '[Discount] Add discount',
    GetDiscountFail = '[Discount] Add discount fail',
    GetDiscountSuccess = '[Discount] Add discount success',

    ApplyDiscount = '[Discount] Apply discount',
    ApplyDiscountFail = '[Discount] Apply discount fail',
    ApplyDiscountSuccess = '[Discount] Apply discount success',

    ClearDiscount = '[Discount] Clear discount',
    ClearDiscountFail = '[Discount] Clear discount fail',
    ClearDiscountSuccess = '[Discount] Clear discount success'
}

export class GetDiscount implements Action {
    readonly type = DiscountActionTypes.GetDiscount;
    constructor() {}
}

export class GetDiscountFail implements Action {
    readonly type = DiscountActionTypes.GetDiscountFail;
    constructor(public payload: any) {}
}

export class GetDiscountSuccess implements Action {
  readonly type = DiscountActionTypes.GetDiscountSuccess;
  constructor(public payload: IDiscount) {}
}

export class ApplyDiscount implements Action {
    readonly type = DiscountActionTypes.ApplyDiscount;
    constructor(public payload: string) {}
}

export class ApplyDiscountFail implements Action {
    readonly type = DiscountActionTypes.ApplyDiscountFail;
    constructor(public payload: any) {}
}

export class ApplyDiscountSuccess implements Action {
  readonly type = DiscountActionTypes.ApplyDiscountSuccess;
  constructor(public payload: IDiscount) {}
}

export class ClearDiscount implements Action {
    readonly type = DiscountActionTypes.ClearDiscount;
}

export class ClearDiscountFail implements Action {
    readonly type = DiscountActionTypes.ClearDiscountFail;
    constructor(public payload: any) {}
}

export class ClearDiscountSuccess implements Action {
  readonly type = DiscountActionTypes.ClearDiscountSuccess;
  constructor() {}
}

export type DiscountActions = GetDiscount
  | GetDiscountFail
  | GetDiscountSuccess
  | ApplyDiscount
  | ApplyDiscountFail
  | ApplyDiscountSuccess
  | ClearDiscount
  | ClearDiscountFail
  | ClearDiscountSuccess;
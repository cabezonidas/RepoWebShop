import { Action } from '@ngrx/store';
import { ITotals } from '../../interfaces/itotals';

export enum TotalsActionTypes {
    GetTotals = '[Totals] Get totals',
    GetTotalsSuccess = '[Totals] Get totals success',
    GetTotalsFail = '[Totals] Get totals fail'
}

export class GetTotals implements Action {
    readonly type = TotalsActionTypes.GetTotals;
}

export class GetTotalsFail implements Action {
    readonly type = TotalsActionTypes.GetTotalsFail;
    constructor(public payload: any) {}
}

export class GetTotalsSuccess implements Action {
  readonly type = TotalsActionTypes.GetTotalsSuccess;
  constructor(public payload: ITotals) {}
}

export type TotalActions =
  | GetTotals
  | GetTotalsFail
  | GetTotalsSuccess;
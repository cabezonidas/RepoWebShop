import { Action } from '@ngrx/store';
import { IInvoiceInfo } from '../../interfaces/iinvoice-info';

export enum InvoiceActionTypes {
    GetCuit = '[Invoice] Get Cuit',
    GetCuitFail = '[Invoice] Get Cuit fail',
    GetCuitSuccess = '[Invoice] Get Cuit success',

    IsCuitValid = '[Invoice] Is Cuit Valid',
    IsCuitValidFail = '[Invoice] Is Cuit Valid fail',
    IsCuitValidSuccess = '[Invoice] Is Cuit Valid success',

    AddCuit= '[Invoice] Add Cuit',
    AddCuitFail = '[Invoice] Add Cuit fail',
    AddCuitSuccess = '[Invoice] Add Cuit success',

    ClearCuit= '[Invoice] Clear Cuit',
    ClearCuitFail = '[Invoice] Clear Cuit fail',
    ClearCuitSuccess = '[Invoice] Clear Cuit success'
}

export class GetCuit implements Action {
    readonly type = InvoiceActionTypes.GetCuit;
    constructor() {}
}

export class GetCuitFail implements Action {
    readonly type = InvoiceActionTypes.GetCuitFail;
    constructor(public payload: any) {}
}

export class GetCuitSuccess implements Action {
  readonly type = InvoiceActionTypes.GetCuitSuccess;
  constructor(public payload: string) {}
}

export class IsCuitValid implements Action {
    readonly type = InvoiceActionTypes.IsCuitValid;
    constructor(public payload: string) {}
}

export class IsCuitValidFail implements Action {
    readonly type = InvoiceActionTypes.IsCuitValidFail;
    constructor(public payload: any) {}
}

export class IsCuitValidSuccess implements Action {
  readonly type = InvoiceActionTypes.IsCuitValidSuccess;
  constructor(public payload: boolean) {}
}

export class AddCuit implements Action {
    readonly type = InvoiceActionTypes.AddCuit;
    constructor(public payload: string) {}
}

export class AddCuitFail implements Action {
    readonly type = InvoiceActionTypes.AddCuitFail;
    constructor(public payload: any) {}
}

export class AddCuitSuccess implements Action {
  readonly type = InvoiceActionTypes.AddCuitSuccess;
  constructor(public payload: string) {}
}

export class ClearCuit implements Action {
    readonly type = InvoiceActionTypes.ClearCuit;
    constructor() {}
}

export class ClearCuitFail implements Action {
    readonly type = InvoiceActionTypes.ClearCuitFail;
    constructor(public payload: any) {}
}

export class ClearCuitSuccess implements Action {
  readonly type = InvoiceActionTypes.ClearCuitSuccess;
  constructor() {}
}

export type InvoiceActions = GetCuit
| GetCuitFail
| GetCuitSuccess 
| IsCuitValid
| IsCuitValidFail
| IsCuitValidSuccess
| AddCuit
| AddCuitFail
| AddCuitSuccess
| ClearCuit
| ClearCuitFail
| ClearCuitSuccess;
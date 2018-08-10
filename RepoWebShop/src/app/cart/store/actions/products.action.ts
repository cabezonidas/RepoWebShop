import { Action } from '@ngrx/store';
import { ICartCatalogItem } from '../../interfaces/icart-catalog-item';

export enum ActionTypes {
    Add = '[Cart] Add product',
    AddSuccess = '[Cart] Add product success',
    AddFail = '[Cart] Add product fail',
    
    Remove = '[Cart] Remove product',
    RemoveSuccess = '[Cart] Remove product success',
    RemoveFail = '[Cart] Remove product fail',

    Load = '[Cart] Load products',
    LoadSuccess = '[Cart] Load products success',
    LoadFail = '[Cart] Load products fail'
}

export class AddProduct implements Action {
    readonly type = ActionTypes.Add;
    constructor(public payload: number) {}
}

export class AddProductFail implements Action {
    readonly type = ActionTypes.AddFail;
    constructor(public payload: any) {}
}

export class AddProductSuccess implements Action {
  readonly type = ActionTypes.AddSuccess;
  constructor(public payload: ICartCatalogItem[]) {}
}

export class RemoveProduct implements Action {
    readonly type = ActionTypes.Remove;
    constructor(public payload: number) {}
}

export class RemoveProductFail implements Action {
    readonly type = ActionTypes.RemoveFail;
    constructor(public payload: any) {}
}

export class RemoveProductSuccess implements Action {
  readonly type = ActionTypes.RemoveSuccess;
  constructor(public payload: ICartCatalogItem[]) {}
}

export class LoadProducts implements Action {
    readonly type = ActionTypes.Load;
}

export class LoadProductsFail implements Action {
    readonly type = ActionTypes.LoadFail;
    constructor(public payload: any) {}
}

export class LoadProductsSuccess implements Action {
  readonly type = ActionTypes.LoadSuccess;
  constructor(public payload: ICartCatalogItem[]) {}
}

export type ProductActions =
  | AddProduct
  | AddProductFail
  | AddProductSuccess
  | RemoveProduct
  | RemoveProductFail
  | RemoveProductSuccess
  | LoadProducts
  | LoadProductsFail
  | LoadProductsSuccess;
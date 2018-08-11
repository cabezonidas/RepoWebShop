import { Action } from '@ngrx/store';
import { ICartCatalogItem } from '../../interfaces/icart-catalog-item';

export enum ItemActionTypes {
    Add = '[Cart] Add item',
    AddSuccess = '[Cart] Add item success',
    AddFail = '[Cart] Add item fail',
    
    Remove = '[Cart] Remove item',
    RemoveSuccess = '[Cart] Remove item success',
    RemoveFail = '[Cart] Remove item fail',

    Load = '[Cart] Load items',
    LoadSuccess = '[Cart] Load items success',
    LoadFail = '[Cart] Load items fail'
}

export class AddItem implements Action {
    readonly type = ItemActionTypes.Add;
    constructor(public payload: number) {}
}

export class AddItemFail implements Action {
    readonly type = ItemActionTypes.AddFail;
    constructor(public payload: any) {}
}

export class AddItemSuccess implements Action {
  readonly type = ItemActionTypes.AddSuccess;
  constructor(public payload: ICartCatalogItem[]) {}
}

export class RemoveItem implements Action {
    readonly type = ItemActionTypes.Remove;
    constructor(public payload: number) {}
}

export class RemoveItemFail implements Action {
    readonly type = ItemActionTypes.RemoveFail;
    constructor(public payload: any) {}
}

export class RemoveItemSuccess implements Action {
  readonly type = ItemActionTypes.RemoveSuccess;
  constructor(public payload: ICartCatalogItem[]) {}
}

export class LoadItems implements Action {
    readonly type = ItemActionTypes.Load;
}

export class LoadItemsFail implements Action {
    readonly type = ItemActionTypes.LoadFail;
    constructor(public payload: any) {}
}

export class LoadItemsSuccess implements Action {
  readonly type = ItemActionTypes.LoadSuccess;
  constructor(public payload: ICartCatalogItem[]) {}
}

export type ItemActions =
  | AddItem
  | AddItemFail
  | AddItemSuccess
  | RemoveItem
  | RemoveItemFail
  | RemoveItemSuccess
  | LoadItems
  | LoadItemsFail
  | LoadItemsSuccess;
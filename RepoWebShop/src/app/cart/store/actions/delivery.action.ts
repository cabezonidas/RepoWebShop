import { Action } from '@ngrx/store';
import { DeliveryAddress } from '../../classes/delivery-address';

export enum DeliveryActionTypes {
    ClearDelivery = '[Delivery] Clear delivery',
    ClearDeliveryFail = '[Delivery] Clear delivery fail',
    ClearDeliverySuccess = '[Delivery] Clear delivery success',
    
    UpdateInstructions = '[Delivery] Update delivery instructions',
    UpdateInstructionsFail = '[Delivery] Update delivery instructions fail',
    UpdateInstructionsSuccess = '[Delivery] Update delivery instructions success',

    SaveDelivery = '[Delivery] Save delivery',
    SaveDeliveryFail = '[Delivery] Save delivery fail',
    SaveDeliverySuccess = '[Delivery] Save delivery success',

    GetDelivery = '[Delivery] Get delivery',
    GetDeliveryFail = '[Delivery] Get delivery fail',
    GetDeliverySuccess = '[Delivery] Get delivery success',
}

export class ClearDelivery implements Action {
    readonly type = DeliveryActionTypes.ClearDelivery;
    constructor() {}
}

export class ClearDeliveryFail implements Action {
    readonly type = DeliveryActionTypes.ClearDeliveryFail;
    constructor(public payload: any) {}
}

export class ClearDeliverySuccess implements Action {
    readonly type = DeliveryActionTypes.ClearDeliverySuccess;
    constructor() {}
}

export class UpdateInstructions implements Action {
    readonly type = DeliveryActionTypes.UpdateInstructions;
    constructor(public payload: DeliveryAddress) {}
}

export class UpdateInstructionsFail implements Action {
    readonly type = DeliveryActionTypes.UpdateInstructionsFail;
    constructor(public payload: any) {}
}

export class UpdateInstructionsSuccess implements Action {
    readonly type = DeliveryActionTypes.UpdateInstructionsSuccess;
    constructor(public payload: DeliveryAddress) {}
}

export class SaveDelivery implements Action {
    readonly type = DeliveryActionTypes.SaveDelivery;
    constructor(public payload: DeliveryAddress) {}
}

export class SaveDeliveryFail implements Action {
    readonly type = DeliveryActionTypes.SaveDeliveryFail;
    constructor(public payload: any) {}
}

export class SaveDeliverySuccess implements Action {
  readonly type = DeliveryActionTypes.SaveDeliverySuccess;
  constructor(public payload: DeliveryAddress) {}
}

export class GetDelivery implements Action {
    readonly type = DeliveryActionTypes.GetDelivery;
    constructor() {}
}

export class GetDeliveryFail implements Action {
    readonly type = DeliveryActionTypes.GetDeliveryFail;
    constructor(public payload: any) {}
}

export class GetDeliverySuccess implements Action {
  readonly type = DeliveryActionTypes.GetDeliverySuccess;
  constructor(public payload: DeliveryAddress) {}
}

export type DeliveryActions = ClearDelivery
    | ClearDeliveryFail
    | ClearDeliverySuccess
    | UpdateInstructions
    | UpdateInstructionsFail
    | UpdateInstructionsSuccess
    | SaveDelivery
    | SaveDeliveryFail
    | SaveDeliverySuccess
    | GetDelivery
    | GetDeliveryFail
    | GetDeliverySuccess;
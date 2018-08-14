import * as fromdelivery from '../actions/delivery.action';
import { DeliveryAddress } from '../../classes/delivery-address';

export interface DeliveryState {
    delivery: DeliveryAddress;
    loaded: boolean;
    loading: boolean;
    error: any;
}

export const initialState: DeliveryState = {
    delivery: null,
    loaded: false,
    loading: false,
    error: null
};

export function reducer(state = initialState, action: fromdelivery.DeliveryActions): DeliveryState {
    switch (action.type) {
        case fromdelivery.DeliveryActionTypes.ClearDelivery: 
        case fromdelivery.DeliveryActionTypes.GetDelivery: 
        case fromdelivery.DeliveryActionTypes.SaveDelivery: 
        case fromdelivery.DeliveryActionTypes.UpdateInstructions: 
        return {
            ...state,
            loading: true,
        };

        case fromdelivery.DeliveryActionTypes.GetDeliverySuccess: 
        case fromdelivery.DeliveryActionTypes.SaveDeliverySuccess: 
        case fromdelivery.DeliveryActionTypes.UpdateInstructionsSuccess: 
        return {
            ...state,
            delivery: action.payload,
            loading: false,
            loaded: true,
        }

        case fromdelivery.DeliveryActionTypes.ClearDeliverySuccess: 
        return {
            ...state,
            delivery: null,
            loading: false,
            loaded: true,
        }

        case fromdelivery.DeliveryActionTypes.ClearDeliveryFail: 
        case fromdelivery.DeliveryActionTypes.GetDeliveryFail: 
        case fromdelivery.DeliveryActionTypes.SaveDeliveryFail: 
        case fromdelivery.DeliveryActionTypes.UpdateInstructionsFail: 
        return {
            ...state,
            error: action.payload,
            loading: false,
            loaded: true,
        }
        default:
            return state;
    }
}

export const getDelivery = (state: DeliveryState) => state.delivery;
export const getDeliveryLoading = (state: DeliveryState) => state.loading;
export const getDeliveryLoaded = (state: DeliveryState) => state.loaded;
export const getDeliveryError = (state: DeliveryState) => state.error;

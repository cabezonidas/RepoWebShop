import * as fromPickup from '../actions/pickup.action';
import { IPickupOption } from '../../components/pickup/interfaces/ipickup-option';
import { IPickupDate } from '../../interfaces/ipickup-date';

export interface PickupState {
    options: IPickupOption[];
    pickup: IPickupDate;
    preparationTime: number | null;
    loaded: boolean;
    loading: boolean;
    error: any;
}

export const initialState: PickupState = {
    options: [],
    pickup: null,
    preparationTime: null,
    loaded: false,
    loading: false,
    error: null
};

export function reducer(state = initialState, action: fromPickup.PickupActions): PickupState {
    switch (action.type) {
        case fromPickup.PickupActionTypes.GetPickupOption:
        case fromPickup.PickupActionTypes.GetPreparationTime:
        case fromPickup.PickupActionTypes.LoadPickupOptions:
        case fromPickup.PickupActionTypes.SetPickupOption:
        return {
            ...state,
            loading: true,
        };

        case fromPickup.PickupActionTypes.GetPickupOptionSuccess:
        return {
            ...state,
            pickup: action.payload,
            loading: false,
            loaded: true,
        };

        case fromPickup.PickupActionTypes.GetPreparationTimeSuccess:
        return {
            ...state,
            preparationTime: action.payload,
            loading: false,
            loaded: true,
        };

        case fromPickup.PickupActionTypes.LoadPickupOptionsSuccess:
        return {
            ...state,
            options: action.payload,
            loading: false,
            loaded: true,
        };

        case fromPickup.PickupActionTypes.SetPickupOptionSuccess:
        return {
            ...state,
            pickup: action.payload,
            loading: false,
            loaded: true,
        };

        case fromPickup.PickupActionTypes.GetPickupOptionFail:
        case fromPickup.PickupActionTypes.GetPreparationTimeFail:
        case fromPickup.PickupActionTypes.LoadPickupOptionsFail:
        case fromPickup.PickupActionTypes.SetPickupOptionFail:
        return {
            ...state,
            error: action.payload,
            loading: false,
            loaded: true,
        };
        default:
            return state;
    }
}

export const getPickup = (state: PickupState) => state.pickup;
export const getOptions = (state: PickupState) => state.options;
export const getPreparationTime = (state: PickupState) => state.preparationTime;
export const getPickupLoading = (state: PickupState) => state.loading;
export const getPickupLoaded = (state: PickupState) => state.loaded;
export const getPickupError = (state: PickupState) => state.error;

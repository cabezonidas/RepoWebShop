import * as fromCaterings from '../actions/custom-catering.action';
import { ICatering } from '../../../catering/interfaces/ICatering';

export interface CustomCateringState {
    catering: ICatering;
    loaded: boolean;
    loading: boolean;
    error: any;
}

export const initialState: CustomCateringState = {
    catering: null,
    loaded: false,
    loading: false,
    error: null
};

export function reducer(state = initialState, action: fromCaterings.CustomCateringActions): CustomCateringState {
    switch (action.type) {
        case fromCaterings.CustomCateringActionTypes.LoadSessionCatering:
        case fromCaterings.CustomCateringActionTypes.RemoveSessionCatering:
        return {
            ...state,
            loading: true,
        };

        case fromCaterings.CustomCateringActionTypes.LoadSessionCateringSuccess: 
        return {
            ...state,
            catering: action.payload,
            loading: false,
            loaded: true,
        }

        case fromCaterings.CustomCateringActionTypes.RemoveSessionCateringSuccess: 
        return {
            ...state,
            catering: null,
            loading: false,
            loaded: true,
        }

        case fromCaterings.CustomCateringActionTypes.LoadSessionCateringFail:
        case fromCaterings.CustomCateringActionTypes.RemoveSessionCateringFail:
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

export const getCustomCatering = (state: CustomCateringState) => state.catering;
export const getCustomCateringLoading = (state: CustomCateringState) => state.loading;
export const getCustomCateringLoaded = (state: CustomCateringState) => state.loaded;
export const getCustomCateringError = (state: CustomCateringState) => state.error;

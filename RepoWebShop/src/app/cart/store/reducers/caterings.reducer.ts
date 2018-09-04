import * as fromCaterings from '../actions/caterings.action';
import { ICartCatering } from '../../interfaces/icart-catering';

export interface CateringState {
    caterings: ICartCatering[];
    loaded: boolean;
    loading: boolean;
    error: any;
}

export const initialState: CateringState = {
    caterings: [],
    loaded: false,
    loading: false,
    error: null
};

export function reducer(state = initialState, action: fromCaterings.CateringActions): CateringState {
    switch (action.type) {
        case fromCaterings.CateringActionTypes.LoadCaterings:
        case fromCaterings.CateringActionTypes.AddCatering:
        case fromCaterings.CateringActionTypes.RemoveCatering:
        case fromCaterings.CateringActionTypes.CopyCatering:
        return {
            ...state,
            loading: true,
        };

        case fromCaterings.CateringActionTypes.CopyCateringSuccess:
        return {
            ...state,
            loading: false,
        };

        case fromCaterings.CateringActionTypes.CopyCateringFail:
        return {
            ...state,
            loading: false,
            error: action.payload
        };

        case fromCaterings.CateringActionTypes.LoadCateringsSuccess:
        case fromCaterings.CateringActionTypes.AddCateringSuccess:
        case fromCaterings.CateringActionTypes.RemoveCateringSuccess:
        return {
            ...state,
            caterings: action.payload,
            loading: false,
            loaded: true,
        };

        case fromCaterings.CateringActionTypes.LoadCateringsFail:
        case fromCaterings.CateringActionTypes.AddCateringFail:
        case fromCaterings.CateringActionTypes.RemoveCateringFail:
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

export const getCaterings = (state: CateringState) => state.caterings;
export const getCateringsLoading = (state: CateringState) => state.loading;
export const getCateringsLoaded = (state: CateringState) => state.loaded;
export const getCateringsError = (state: CateringState) => state.error;

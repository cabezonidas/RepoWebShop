import { CateringActions, CateringActionTypes } from './catering.actions';
import { IItem } from '../../products/interfaces/iitem';
import { ICatering } from '../interfaces/ICatering';

export interface CateringState {
    items: IItem[];
    catering: ICatering | null;
    error: string;
    loading: boolean;
    loadingCaterings: boolean;
    caterings: ICatering[];
}

const initialState: CateringState = {
    items: [],
    catering: null,
    error: '',
    loading: false,
    loadingCaterings: false,
    caterings: []
};

export function reducer(state = initialState, action: CateringActions): CateringState {
    switch (action.type) {
        case CateringActionTypes.LoadItems: return {
            ...state,
            loading: true,
            error: ''
        };

        case CateringActionTypes.AddItem: return {
            ...state,
            loading: true,
            error: ''
        };

        case CateringActionTypes.RemoveItem: return {
            ...state,
            loading: true,
            error: ''
        };

        case CateringActionTypes.LoadCaterings: return {
            ...state,
            loadingCaterings: true,
            error: ''
        };

        case CateringActionTypes.LoadSessionCatering: return {
            ...state,
            loading: true,
            error: ''
        };

        case CateringActionTypes.LoadItemsSuccess: return {
            ...state,
            items: action.payload,
            loading: false,
            error: ''
        };
        case CateringActionTypes.LoadItemsFail: return {
            ...state,
            items: [],
            loading: false,
            error: action.payload
        };
        case CateringActionTypes.LoadSessionCateringSuccess: return {
            ...state,
            catering: action.payload,
            loading: false,
            error: ''
        };
        case CateringActionTypes.LoadSessionCateringFail: return {
            ...state,
            catering: null,
            loading: false,
            error: action.payload
        };
        case CateringActionTypes.LoadCateringsSuccess: return {
            ...state,
            caterings: action.payload,
            loadingCaterings: false,
            error: ''
        };
        case CateringActionTypes.LoadCateringsFail: return {
            ...state,
            caterings: null,
            loadingCaterings: false,
            error: action.payload
        };
        default:
            return state;
    }
}

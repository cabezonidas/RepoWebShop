import { CateringActions, CateringActionTypes } from './catering.actions';
import { IItem } from '../../products/interfaces/iitem';
import { ICatering } from '../interfaces/ICatering';

export interface CateringState {
    error: string;
    loading: boolean;
    cateringsLoaded: boolean;
    loadingCaterings: boolean;
    caterings: ICatering[];
    productsSelected: IItem[];
}

const initialState: CateringState = {
    error: '',
    loading: false,
    cateringsLoaded: false,
    loadingCaterings: false,
    caterings: [],
    productsSelected: []
};

export function reducer(state = initialState, action: CateringActions): CateringState {
    switch (action.type) {

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

        case CateringActionTypes.AddLocalItem: return {
            ...state,
            productsSelected: state.productsSelected.concat(action.payload)
        };

        case CateringActionTypes.RemoveLocalItem: {
            const index = state.productsSelected.lastIndexOf(action.payload);
            return {
                ...state,
                productsSelected: index >= 0 ? state.productsSelected.filter((item, i) => i !== index) : state.productsSelected
            };
        }

        case CateringActionTypes.LoadSessionCateringDone: return {
            ...state,
            loading: false,
            error: ''
        };

        case CateringActionTypes.LoadCaterings: return {
            ...state,
            loadingCaterings: true,
            cateringsLoaded: false,
            error: ''
        };

        case CateringActionTypes.LoadCateringsSuccess: return {
            ...state,
            caterings: action.payload,
            loadingCaterings: false,
            cateringsLoaded: true,
            error: ''
        };
        case CateringActionTypes.LoadCateringsFail: return {
            ...state,
            caterings: null,
            loadingCaterings: false,
            cateringsLoaded: true,
            error: action.payload
        };
        default:
            return state;
    }
}

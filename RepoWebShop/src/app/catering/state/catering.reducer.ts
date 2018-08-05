import { CateringActions, CateringActionTypes } from './catering.actions';
import { IItem } from '../../products/interfaces/iitem';
import { ICatering } from '../interfaces/ICatering';

export interface CateringState {
    items: IItem[];
    catering: ICatering | null;
    error: string;
}

const initialState: CateringState = {
    items: [],
    catering: null,
    error: ''
};

export function reducer(state = initialState, action: CateringActions): CateringState {
    switch (action.type) {
        case CateringActionTypes.LoadItemsSuccess: return {
            ...state,
            items: action.payload,
            error: ''
        };
        case CateringActionTypes.LoadItemsFail: return {
            ...state,
            items: [],
            error: action.payload
        };
        case CateringActionTypes.LoadSessionCateringSuccess: return {
            ...state,
            catering: action.payload,
            error: ''
        };
        case CateringActionTypes.LoadSessionCateringFail: return {
            ...state,
            catering: null,
            error: action.payload
        };
        default:
            return state;
    }
}

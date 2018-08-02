import { CateringActions, CateringActionTypes } from './catering.actions';
import { IItem } from '../../products/interfaces/iitem';

export interface CateringState {
    items: IItem[];
    error: string;
}

const initialState: CateringState = {
    items: [],
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
        default:
            return state;
    }
}

import * as fromDiscounts from '../actions/discounts.action';
import { IDiscount } from '../../interfaces/idiscount';

export interface DiscountState {
    discount: IDiscount | null;
    loaded: boolean;
    loading: boolean;
    error: any;
}

export const initialState: DiscountState = {
    discount: null,
    loaded: false,
    loading: false,
    error: null
};

export function reducer(state = initialState, action: fromDiscounts.DiscountActions): DiscountState {
    switch (action.type) {
        case fromDiscounts.DiscountActionTypes.Get: 
        case fromDiscounts.DiscountActionTypes.Apply:
        return {
            ...state,
            loading: true,
        };

        case fromDiscounts.DiscountActionTypes.GetSuccess: 
        case fromDiscounts.DiscountActionTypes.ApplySuccess: 
        return {
            ...state,
            discount: action.payload,
            loading: false,
            loaded: true,
        }

        case fromDiscounts.DiscountActionTypes.GetFail: 
        case fromDiscounts.DiscountActionTypes.ApplyFail: 
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

export const getDiscount = (state: DiscountState) => state.discount;
export const getDiscountLoading = (state: DiscountState) => state.loading;
export const getDiscountLoaded = (state: DiscountState) => state.loaded;
export const getDiscountError = (state: DiscountState) => state.error;

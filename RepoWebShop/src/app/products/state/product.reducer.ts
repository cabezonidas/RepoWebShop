import { IProduct } from '../interfaces/iproduct';
import { ProductActions, ProductActionTypes } from './product.actions';

export interface ProductState {
    products: IProduct[];
    showProductCode: boolean;
    currentProductId: number | null;
    error: string;
}

const initialState: ProductState = {
    products: [],
    showProductCode: true,
    currentProductId: null,
    error: ''
};

export function reducer(state = initialState, action: ProductActions): ProductState {
    switch (action.type) {
        case ProductActionTypes.LoadSuccess: return {
            ...state,
            products: action.payload,
            error: ''
        };

        case ProductActionTypes.LoadFail: return {
            ...state,
            products: [],
            error: action.payload
        };

        default:
            return state;
    }
}

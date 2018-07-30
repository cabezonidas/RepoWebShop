import { IProduct } from '../interfaces/iproduct';
import * as fromRoot from '../../state/app.state';
import { createFeatureSelector, createSelector } from '@ngrx/store';
import { ProductActions, ProductActionTypes } from './product.action';

export interface State extends fromRoot.State {
    products: ProductState;
}

export interface ProductState {
    products: IProduct[];
    showProductCode: boolean;
    currentProduct: IProduct;
    error: string;
}

const initialState: ProductState = {
    products: [],
    showProductCode: true,
    currentProduct: null,
    error: ''
};

const getProductsFeatureState = createFeatureSelector<ProductState>('product');

export const getProducts = createSelector(
    getProductsFeatureState,
    state => state.products
);

export const getError = createSelector(
    getProductsFeatureState,
    state => state.error
);

export const toggleProductCode = createSelector(
    getProductsFeatureState,
    state => state.showProductCode
);

export function reducer(state = initialState, action: ProductActions): ProductState {
    switch (action.type) {
        case ProductActionTypes.ToggleProductCode: return {
            ...state,
            showProductCode: action.payload
        };
        case ProductActionTypes.SetCurrentProduct: return {
            ...state,
            currentProduct: { ...action.payload }
        };
        case ProductActionTypes.ClearCurrentProduct: return {
            ...state,
            currentProduct: null
        };
        // case ProductActionTypes.InitializeCurrentProduct: return {
        //     ...state,
        //     currentProduct: {
        //         pieDetailId: 1,
        //         primaryPicture: ''
        //     }
        // };
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

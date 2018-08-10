import * as fromProducts from '../actions/products.action';
import { ICartCatalogItem } from '../../interfaces/icart-catalog-item';

export interface ProductState {
    products: ICartCatalogItem[];
    loaded: boolean;
    loading: boolean;
    error: any;
}

export const initialState: ProductState = {
    products: [],
    loaded: false,
    loading: false,
    error: null
};

export function reducer(state = initialState, action: fromProducts.ProductActions): ProductState {
    switch (action.type) {
        case fromProducts.ActionTypes.Load: 
        case fromProducts.ActionTypes.Add: 
        case fromProducts.ActionTypes.Remove: 
        return {
            ...state,
            loading: true,
        };

        case fromProducts.ActionTypes.LoadSuccess: 
        case fromProducts.ActionTypes.AddSuccess: 
        case fromProducts.ActionTypes.RemoveSuccess: 
        return {
            ...state,
            products: action.payload,
            loading: false,
            loaded: true,
        }

        case fromProducts.ActionTypes.LoadFail: 
        case fromProducts.ActionTypes.AddFail: 
        case fromProducts.ActionTypes.RemoveFail: 
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

export const getProducts = (state: ProductState) => state.products;
export const getProductsLoading = (state: ProductState) => state.loading;
export const getProductsLoaded = (state: ProductState) => state.loaded;
export const getProductsError = (state: ProductState) => state.error;

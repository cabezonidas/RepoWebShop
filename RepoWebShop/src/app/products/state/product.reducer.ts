import { IProduct } from '../interfaces/iproduct';
import { ProductActions, ProductActionTypes } from './product.actions';
import { IAlbum } from '../interfaces/ialbum';

export interface ProductState {
    products: IProduct[];
    albums: IAlbum[];
    showProductCode: boolean;
    currentProductId: number | null;
    error: string;
}

const initialState: ProductState = {
    products: [],
    albums: [],
    showProductCode: true,
    currentProductId: null,
    error: ''
};

export function reducer(state = initialState, action: ProductActions): ProductState {
    switch (action.type) {
        case ProductActionTypes.LoadProductsSuccess: return {
            ...state,
            products: action.payload,
            error: ''
        };
        case ProductActionTypes.LoadProductsFail: return {
            ...state,
            products: [],
            error: action.payload
        };
        case ProductActionTypes.LoadAlbumSuccess: return {
            ...state,
            albums: [...state.albums, action.payload],
            error: ''
        };
        case ProductActionTypes.LoadProductsFail: return {
            ...state,
            error: action.payload
        };

        default:
            return state;
    }
}

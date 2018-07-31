import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromRoot from '../../state/app.state';
import * as fromProducts from './product.reducer';

// Extends the app state to include the product feature.
// This is required because products are lazy loaded.
// So the reference to ProductState cannot be added to app.state.ts directly.
export interface State extends fromRoot.State {
    products: fromProducts.ProductState;
}

// Selector functions
const getProductFeatureState = createFeatureSelector<fromProducts.ProductState>('product');

export const getShowProductCode = createSelector(
    getProductFeatureState,
    state => state.showProductCode
);

export const getCurrentProductId = createSelector(
    getProductFeatureState,
    state => state.currentProductId
);

export const getCurrentProduct = createSelector(
    getProductFeatureState,
    getCurrentProductId,
    (state, currentProductId) => currentProductId ?
        state.products.find(p => p.pieDetailId === currentProductId) : null
);

export const getProducts = createSelector(
    getProductFeatureState,
    state => state.products
);

export const getError = createSelector(
    getProductFeatureState,
    state => state.error
);

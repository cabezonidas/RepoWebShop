import { createSelector } from '@ngrx/store';
import * as fromFeature from '../reducers';
import * as fromProducts from '../reducers/products.reducer';

export const getProductState = createSelector(
    fromFeature.getCartState,
    (state: fromFeature.CartState) => state.products
  );

  export const getProducts = createSelector(
    getProductState,
    fromProducts.getProducts
  );

  export const getProductsLoaded = createSelector(
    getProductState,
    fromProducts.getProductsLoaded
  );
  export const getProductsLoading = createSelector(
    getProductState,
    fromProducts.getProductsLoading
  );
import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';

import * as fromProducts from './products.reducer';

export interface CartState {
  products: fromProducts.ProductState;
}

export const reducers: ActionReducerMap<CartState> = {
  products: fromProducts.reducer,
};

export const getCartState = createFeatureSelector<CartState>(
  'cart'
);
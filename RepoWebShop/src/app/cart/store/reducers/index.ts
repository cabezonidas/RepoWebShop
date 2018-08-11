import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';

import * as fromItems from './items.reducer';
import * as fromCaterings from './caterings.reducer';

export interface CartState {
  items: fromItems.ItemState;
  caterings: fromCaterings.CateringState;
}

export const reducers: ActionReducerMap<CartState> = {
  items: fromItems.reducer,
  caterings: fromCaterings.reducer,
};

export const getCartState = createFeatureSelector<CartState>(
  'cart'
);
import { createSelector } from '@ngrx/store';
import * as fromFeature from '../reducers';
import * as fromItems from '../reducers/items.reducer';

export const getItemState = createSelector(
    fromFeature.getCartState,
    (state: fromFeature.CartState) => state.items
  );

  export const getItems = createSelector(
    getItemState,
    fromItems.getItems
  );

  export const getItemsLoaded = createSelector(
    getItemState,
    fromItems.getItemsLoaded
  );
  export const getItemsLoading = createSelector(
    getItemState,
    fromItems.getItemsLoading
  );

import { createSelector } from '@ngrx/store';
import * as fromFeature from '../reducers';
import * as fromCaterings from '../reducers/caterings.reducer';

export const getCateringState = createSelector(
    fromFeature.getCartState,
    (state: fromFeature.CartState) => state.caterings
  );

  export const getCaterings = createSelector(
    getCateringState,
    fromCaterings.getCaterings
  );

  export const getCateringsLoaded = createSelector(
    getCateringState,
    fromCaterings.getCateringsLoaded
  );
  export const getCateringsLoading = createSelector(
    getCateringState,
    fromCaterings.getCateringsLoading
  );

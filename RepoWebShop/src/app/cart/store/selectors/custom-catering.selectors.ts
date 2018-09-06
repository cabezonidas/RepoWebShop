import { createSelector } from '@ngrx/store';
import * as fromFeature from '../reducers';
import * as fromCustomCatering from '../reducers/custom-catering.reducer';

export const getCustomCateringState = createSelector(
    fromFeature.getCartState,
    (state: fromFeature.CartState) => state.customCatering
  );

  export const getCustomCatering = createSelector(
    getCustomCateringState,
    fromCustomCatering.getCustomCatering
  );

  export const getCustomCateringLoaded = createSelector(
    getCustomCateringState,
    fromCustomCatering.getCustomCateringLoaded
  );
  export const getCustomCateringLoading = createSelector(
    getCustomCateringState,
    fromCustomCatering.getCustomCateringLoading
  );

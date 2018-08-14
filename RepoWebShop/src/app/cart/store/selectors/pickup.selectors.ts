import { createSelector } from '@ngrx/store';
import * as fromFeature from '../reducers';
import * as fromPickup from '../reducers/pickup.reducer';

export const getPickupState = createSelector(
    fromFeature.getCartState,
    (state: fromFeature.CartState) => state.pickup
  );

  export const getPickup = createSelector(
    getPickupState,
    fromPickup.getPickup
  );

  export const getPickupOptions = createSelector(
    getPickupState,
    fromPickup.getOptions
  );

  export const getPreparationTime = createSelector(
    getPickupState,
    fromPickup.getPreparationTime
  );

  export const getPickupLoaded = createSelector(
    getPickupState,
    fromPickup.getPickupLoaded
  );
  export const getPickupLoading = createSelector(
    getPickupState,
    fromPickup.getPickupLoading
  );
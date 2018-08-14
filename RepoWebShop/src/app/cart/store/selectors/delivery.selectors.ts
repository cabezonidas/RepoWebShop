import { createSelector } from '@ngrx/store';
import * as fromFeature from '../reducers';
import * as fromDelivery from '../reducers/delivery.reducer';

export const getDeliveryState = createSelector(
    fromFeature.getCartState,
    (state: fromFeature.CartState) => state.delivery
  );

  export const getDelivery = createSelector(
    getDeliveryState,
    fromDelivery.getDelivery
  );

  export const getDeliveryLoaded = createSelector(
    getDeliveryState,
    fromDelivery.getDeliveryLoaded
  );
  export const getDeliveryLoading = createSelector(
    getDeliveryState,
    fromDelivery.getDeliveryLoading
  );
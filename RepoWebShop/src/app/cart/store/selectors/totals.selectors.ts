import { createSelector } from '@ngrx/store';
import * as fromFeature from '../reducers';
import * as fromTotals from '../reducers/totals.reducer';

export const getTotalState = createSelector(
    fromFeature.getCartState,
    (state: fromFeature.CartState) => state.totals
  );

export const getTotal = createSelector(getTotalState, fromTotals.getTotal);
export const getTotalInStore = createSelector(getTotalState, fromTotals.getTotalInStore);
export const getTotalWithoutDiscount = createSelector(getTotalState, fromTotals.getTotalWithoutDiscount);
export const getTotalItems = createSelector(getTotalState, fromTotals.getItems);
export const getTotalItemsInStore = createSelector(getTotalState, fromTotals.getItemsInStore);
export const getTotalCustomCatering = createSelector(getTotalState, fromTotals.getCustomCatering);
export const getTotalCustomCateringInStore = createSelector(getTotalState, fromTotals.getCustomCateringInStore);
export const getTotalCaterings = createSelector(getTotalState, fromTotals.getCaterings);
export const getTotalCateringsInStore = createSelector(getTotalState, fromTotals.getCateringsInStore);
export const getTotalCateringsSavings = createSelector(getTotalState, fromTotals.getCateringsSavings);

export const getTotalsLoaded = createSelector(
  getTotalState,
  fromTotals.getTotalsLoaded
);
export const getTotalsLoading = createSelector(
  getTotalState,
  fromTotals.getTotalsLoading
);


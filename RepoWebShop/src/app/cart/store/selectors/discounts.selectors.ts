import { createSelector } from '@ngrx/store';
import * as fromFeature from '../reducers';
import * as fromDiscounts from '../reducers/discounts.reducer';

export const getDiscountsState = createSelector(
    fromFeature.getCartState,
    (state: fromFeature.CartState) => state.discount
  );

export const getDiscounts = createSelector(
  getDiscountsState,
  fromDiscounts.getDiscount
);

export const getDiscountsLoaded = createSelector(
  getDiscountsState,
  fromDiscounts.getDiscountLoaded
);
export const getDiscountsLoading = createSelector(
  getDiscountsState,
  fromDiscounts.getDiscountLoading
);
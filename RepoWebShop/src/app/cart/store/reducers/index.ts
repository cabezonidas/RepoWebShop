import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';

import * as fromItems from './items.reducer';
import * as fromCaterings from './caterings.reducer';
import * as fromComments from './comments.reducer';
import * as fromDiscounts from './discounts.reducer';
import * as fromInvoice from './invoice.reducer';
import * as fromPickup from './pickup.reducer';
import * as fromTotals from './totals.reducer';
import * as fromCustomCatering from './custom-catering.reducer';

export interface CartState {
  items: fromItems.ItemState;
  caterings: fromCaterings.CateringState;
  comments: fromComments.CommentState;
  discount: fromDiscounts.DiscountState;
  invoice: fromInvoice.InvoiceState;
  pickup: fromPickup.PickupState;
  totals: fromTotals.TotalState;
  customCatering: fromCustomCatering.CustomCateringState;
}

export const reducers: ActionReducerMap<CartState> = {
  items: fromItems.reducer,
  caterings: fromCaterings.reducer,
  comments: fromComments.reducer,
  discount: fromDiscounts.reducer,
  invoice: fromInvoice.reducer,
  pickup: fromPickup.reducer,
  totals: fromTotals.reducer,
  customCatering: fromCustomCatering.reducer,
};

export const getCartState = createFeatureSelector<CartState>(
  'cart'
);
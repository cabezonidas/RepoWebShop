import { createSelector } from '@ngrx/store';
import * as fromFeature from '../reducers';
import * as fromInvoice from '../reducers/invoice.reducer';

export const getInvoiceState = createSelector(
    fromFeature.getCartState,
    (state: fromFeature.CartState) => state.invoice
  );

  // export const getTaxpayer = createSelector(
  //   getInvoiceState,
  //   fromInvoice.getTaxpayer
  // );


  export const getCuit = createSelector(
    getInvoiceState,
    fromInvoice.getCuit
  );

  export const getInvoiceLoaded = createSelector(
    getInvoiceState,
    fromInvoice.getInvoicesLoaded
  );
  export const getInvoicesLoading = createSelector(
    getInvoiceState,
    fromInvoice.getInvoicesLoading
  );
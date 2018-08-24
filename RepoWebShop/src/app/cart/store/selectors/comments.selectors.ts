import { createSelector } from '@ngrx/store';
import * as fromFeature from '../reducers';
import * as fromComments from '../reducers/comments.reducer';

export const getCommentsState = createSelector(
    fromFeature.getCartState,
    (state: fromFeature.CartState) => state.comments
  );

export const getComments = createSelector(
  getCommentsState,
  fromComments.getComments
);

export const getCommentsLoaded = createSelector(
  getCommentsState,
  fromComments.getCommentsLoaded
);
export const getCommentsLoading = createSelector(
  getCommentsState,
  fromComments.getCommentsLoading
);

import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromRoot from '../../state/app.state';
import * as fromCatering from './catering.reducer';

export interface State extends fromRoot.State {
    catering: fromCatering.CateringState;
}

const getCateringFeatureState = createFeatureSelector<fromCatering.CateringState>('catering');

export const getError = createSelector(
    getCateringFeatureState,
    state => state.error
);
export const getLoading = createSelector(
    getCateringFeatureState,
    state => state.loading
);
export const getCateringsLoaded = createSelector(
    getCateringFeatureState,
    state => state.cateringsLoaded
);
export const getCaterings = createSelector(
    getCateringFeatureState,
    state => state.caterings
);

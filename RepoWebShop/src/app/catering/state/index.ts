import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromRoot from '../../state/app.state';
import * as fromCatering from './catering.reducer';

export interface State extends fromRoot.State {
    catering: fromCatering.CateringState;
}

const getCateringFeatureState = createFeatureSelector<fromCatering.CateringState>('catering');

export const getItems = createSelector(
    getCateringFeatureState,
    state => state.items
);
export const getError = createSelector(
    getCateringFeatureState,
    state => state.error
);

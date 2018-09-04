import { Injectable } from '@angular/core';

import { Effect, Actions } from '@ngrx/effects';
import { map, switchMap, catchError, share } from 'rxjs/operators';
import { of } from 'rxjs';

import * as customCateringActions from '../actions/custom-catering.action';
import * as fromServices from '../../services';
import * as fromTotals from '../actions/totals.action';
import * as fromPickup from '../actions/pickup.action';

@Injectable()
export class CustomCateringEffects {
  constructor(
    private actions$: Actions,
    private customCateringService: fromServices.CustomCateringService
  ) {}

  @Effect()
  loadSessionCatering$ = this.actions$.ofType(customCateringActions.CustomCateringActionTypes.LoadSessionCatering).pipe(
    switchMap(() => {
      return this.customCateringService
        .loadSessionCatering()
        .pipe(
          map(catering => new customCateringActions.LoadSessionCateringSuccess(catering)),
          catchError(error => of(new customCateringActions.LoadSessionCateringFail(error)))
        );
    }),
    share()
  );

  @Effect()
  removeSessionCatering$ = this.actions$.ofType(customCateringActions.CustomCateringActionTypes.RemoveSessionCatering).pipe(
    switchMap(() => {
      return this.customCateringService
        .clearSessionCatering()
        .pipe(
          switchMap(() => [
            new customCateringActions.RemoveSessionCateringSuccess(),
            new fromTotals.GetTotals(),
            new fromPickup.LoadPickupOptions(),
            new fromPickup.GetPickupOption(),
            new fromPickup.GetPreparationTime()
          ]),
          catchError(error => of(new customCateringActions.RemoveSessionCateringFail(error)))
        );
    })
  );
}

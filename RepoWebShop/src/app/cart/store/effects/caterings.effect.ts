import { Injectable } from '@angular/core';
import * as fromTotals from '../actions/totals.action';

import { Effect, Actions } from '@ngrx/effects';
import { map, switchMap, catchError, tap, share } from 'rxjs/operators';
import { of } from 'rxjs';

import * as cateringActions from '../actions/caterings.action';
import * as fromServices from '../../services';
import * as fromPickup from '../actions/pickup.action';

@Injectable()
export class CateringsEffects {
  constructor(
    private actions$: Actions,
    private cateringService: fromServices.CateringsService
  ) {}

  @Effect()
  loadCaterings$ = this.actions$.ofType(cateringActions.CateringActionTypes.LoadCaterings).pipe(
    switchMap(() => {
      return this.cateringService
        .getCartCaterings()
        .pipe(
          map(caterings => new cateringActions.LoadCateringsSuccess(caterings)),
          catchError(error => of(new cateringActions.LoadCateringsFail(error)))
        );
    })
  );

  @Effect()
  addCatering$ = this.actions$.ofType(cateringActions.CateringActionTypes.AddCatering).pipe(
    map((action: cateringActions.AddCatering) => action.payload),
    switchMap(cateringId => {
      return this.cateringService
        .addCatering(cateringId)
        .pipe(
          switchMap(items => [
            new cateringActions.AddCateringSuccess(items),
            new fromTotals.GetTotals(),
            new fromPickup.LoadPickupOptions(),
            new fromPickup.GetPickupOption(),
            new fromPickup.GetPreparationTime()
          ]),
          catchError(error => of(new cateringActions.AddCateringFail(error)))
        );
    }),
    share()
  );

  @Effect()
  removeCatering$ = this.actions$.ofType(cateringActions.CateringActionTypes.RemoveCatering).pipe(
    map((action: cateringActions.RemoveCatering) => action.payload),
    switchMap(cateringId => {
      return this.cateringService
        .removeCatering(cateringId)
        .pipe(
          switchMap(items => [
            new cateringActions.RemoveCateringSuccess(items),
            new fromTotals.GetTotals(),
            new fromPickup.LoadPickupOptions(),
            new fromPickup.GetPickupOption(),
            new fromPickup.GetPreparationTime()
          ]),
          catchError(error => of(new cateringActions.RemoveCateringFail(error)))
        );
    }),
    share()
  );
}

import { Injectable } from '@angular/core';

import { Effect, Actions } from '@ngrx/effects';
import { map, switchMap, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

import * as cateringActions from '../actions/caterings.action';
import * as fromServices from '../../services';

@Injectable()
export class CateringsEffects {
  constructor(
    private actions$: Actions,
    private cateringService: fromServices.CateringsService
  ) {}

  @Effect()
  loadItems$ = this.actions$.ofType(cateringActions.CateringActionTypes.Load).pipe(
    switchMap(() => {
      return this.cateringService
        .getCartCaterings()
        .pipe(
          map(items => new cateringActions.LoadCateringsSuccess(items)),
          catchError(error => of(new cateringActions.LoadCateringsFail(error)))
        );
    })
  );

  @Effect()
  addCatering$ = this.actions$.ofType(cateringActions.CateringActionTypes.Add).pipe(
    map((action: cateringActions.AddCatering) => action.payload),
    switchMap(cateringId => {
      return this.cateringService
        .addCatering(cateringId)
        .pipe(
          map(items => new cateringActions.AddCateringSuccess(items)),
          catchError(error => of(new cateringActions.AddCateringFail(error)))
        );
    })
  );

  @Effect()
  removeCatering$ = this.actions$.ofType(cateringActions.CateringActionTypes.Remove).pipe(
    map((action: cateringActions.RemoveCatering) => action.payload),
    switchMap(cateringId => {
      return this.cateringService
        .removeCatering(cateringId)
        .pipe(
          map(items => new cateringActions.RemoveCateringSuccess(items)),
          catchError(error => of(new cateringActions.RemoveCateringFail(error)))
        );
    })
  );
}
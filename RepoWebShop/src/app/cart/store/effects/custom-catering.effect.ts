import { Injectable } from '@angular/core';

import { Effect, Actions } from '@ngrx/effects';
import { map, switchMap, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

import * as customCateringActions from '../actions/custom-catering.action';
import * as fromServices from '../../services';

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
    })
  );

  @Effect()
  removeSessionCatering$ = this.actions$.ofType(customCateringActions.CustomCateringActionTypes.RemoveSessionCatering).pipe(
    switchMap(() => {
      return this.customCateringService
        .clearSessionCatering()
        .pipe(
          map(() => new customCateringActions.RemoveSessionCateringSuccess()),
          catchError(error => of(new customCateringActions.RemoveSessionCateringFail(error)))
        );
    })
  );
}
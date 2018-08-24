import { Injectable } from '@angular/core';

import { Effect, Actions } from '@ngrx/effects';
import { map, switchMap, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

import * as totalActions from '../actions/totals.action';
import * as fromServices from '../../services';

@Injectable()
export class TotalsEffects {
  constructor(
    private actions$: Actions,
    private totalService: fromServices.TotalsService
  ) {}

  @Effect()
  loadTotals$ = this.actions$.ofType(totalActions.TotalsActionTypes.GetTotals).pipe(
    switchMap(() => {
      return this.totalService
        .getTotals()
        .pipe(
          map(totals => new totalActions.GetTotalsSuccess(totals)),
          catchError(error => of(new totalActions.GetTotalsFail(error)))
        );
    })
  );
}

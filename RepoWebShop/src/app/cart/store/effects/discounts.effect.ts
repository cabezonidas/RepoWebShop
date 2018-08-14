import { Injectable } from '@angular/core';

import { Effect, Actions } from '@ngrx/effects';
import { map, switchMap, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

import * as discountActions from '../actions/discounts.action';
import * as fromServices from '../../services';

@Injectable()
export class DiscountsEffects {
  constructor(
    private actions$: Actions,
    private discountService: fromServices.DiscountService
  ) {}

  @Effect()
  getDiscount$ = this.actions$.ofType(discountActions.DiscountActionTypes.Get).pipe(
    switchMap(() => {
      return this.discountService.get()
        .pipe(
          map(discounts => new discountActions.GetSuccess(discounts)),
          catchError(error => of(new discountActions.GetFail(error)))
        );
    })
  );

  @Effect()
  addDiscount$ = this.actions$.ofType(discountActions.DiscountActionTypes.Apply).pipe(
    map((action: discountActions.Apply) => action.payload),
    switchMap(discountId => {
      return this.discountService
        .apply(discountId)
        .pipe(
          map(items => new discountActions.ApplySuccess(items)),
          catchError(error => of(new discountActions.ApplyFail(error)))
        );
    })
  );
}
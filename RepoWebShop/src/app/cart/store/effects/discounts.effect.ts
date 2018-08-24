import { Injectable } from '@angular/core';

import { Effect, Actions } from '@ngrx/effects';
import { map, switchMap, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

import * as discountActions from '../actions/discounts.action';
import * as fromServices from '../../services';
import * as fromTotals from '../actions/totals.action';
import * as fromPickup from '../actions/pickup.action';

@Injectable()
export class DiscountsEffects {
  constructor(
    private actions$: Actions,
    private discountService: fromServices.DiscountService
  ) {}

  @Effect()
  getDiscount$ = this.actions$.ofType(discountActions.DiscountActionTypes.GetDiscount).pipe(
    switchMap(() => {
      return this.discountService.get()
        .pipe(
          map(discounts => new discountActions.GetDiscountSuccess(discounts)),
          catchError(error => of(new discountActions.GetDiscountFail(error)))
        );
    })
  );

  @Effect()
  addDiscount$ = this.actions$.ofType(discountActions.DiscountActionTypes.ApplyDiscount).pipe(
    map((action: discountActions.ApplyDiscount) => action.payload),
    switchMap(discountId => {
      return this.discountService
        .apply(discountId)
        .pipe(
          map(items => [
            new discountActions.ApplyDiscountSuccess(items),
            new fromTotals.GetTotals(),
            new fromPickup.LoadPickupOptions(),
            new fromPickup.GetPickupOption()
          ]),
          catchError(error => of(new discountActions.ApplyDiscountFail(error)))
        );
    })
  );

  @Effect()
  clearDiscount$ = this.actions$.ofType(discountActions.DiscountActionTypes.ClearDiscount).pipe(
    switchMap(() => {
      return this.discountService
        .clear()
        .pipe(
          map(() => [
            new discountActions.ClearDiscountSuccess(),
            new fromTotals.GetTotals(),
            new fromPickup.LoadPickupOptions(),
            new fromPickup.GetPickupOption()
          ]),
          catchError(error => of(new discountActions.ClearDiscountFail(error)))
        );
    })
  );
}

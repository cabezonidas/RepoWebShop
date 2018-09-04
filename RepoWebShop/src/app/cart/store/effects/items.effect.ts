import { Injectable } from '@angular/core';

import { Effect, Actions } from '@ngrx/effects';
import { map, switchMap, catchError, share } from 'rxjs/operators';
import { of } from 'rxjs';

import * as itemActions from '../actions/items.action';
import * as fromServices from '../../services';
import * as fromTotals from '../actions/totals.action';
import * as fromPickup from '../actions/pickup.action';

@Injectable()
export class ItemsEffects {
  constructor(
    private actions$: Actions,
    private itemService: fromServices.ItemsService
  ) {}

  @Effect()
  loadItems$ = this.actions$.ofType(itemActions.ItemActionTypes.Load).pipe(
    switchMap(() => {
      return this.itemService
        .getCartItems()
        .pipe(
          map(items => new itemActions.LoadItemsSuccess(items)),
          catchError(error => of(new itemActions.LoadItemsFail(error)))
        );
    })
  );

  @Effect()
  addItem$ = this.actions$.ofType(itemActions.ItemActionTypes.Add).pipe(
    map((action: itemActions.AddItem) => action.payload),
    switchMap(itemId => {
      return this.itemService
        .addItem(itemId)
        .pipe(
          switchMap(items => [
            new itemActions.AddItemSuccess(items),
            new fromTotals.GetTotals(),
            new fromPickup.LoadPickupOptions(),
            new fromPickup.GetPickupOption(),
            new fromPickup.GetPreparationTime()
          ]),
          catchError(error => of(new itemActions.AddItemFail(error)))
        );
    }),
    share()
  );

  @Effect()
  removeItem$ = this.actions$.ofType(itemActions.ItemActionTypes.Remove).pipe(
    map((action: itemActions.RemoveItem) => action.payload),
    switchMap(itemId => {
      return this.itemService
        .removeItem(itemId)
        .pipe(
          switchMap(items => [
            new itemActions.RemoveItemSuccess(items),
            new fromTotals.GetTotals(),
            new fromPickup.LoadPickupOptions(),
            new fromPickup.GetPickupOption(),
            new fromPickup.GetPreparationTime()
          ]),
          catchError(error => of(new itemActions.RemoveItemFail(error)))
        );
    }),
    share()
  );
}

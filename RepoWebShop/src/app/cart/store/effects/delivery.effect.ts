import { Injectable } from '@angular/core';

import { Effect, Actions } from '@ngrx/effects';
import { map, switchMap, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

import * as deliveryActions from '../actions/delivery.action';
import * as fromServices from '../../services';
import * as fromTotals from '../actions/totals.action';
import * as fromPickup from '../actions/pickup.action';

@Injectable()
export class DeliveryEffects {
  constructor(
    private actions$: Actions,
    private deliveryService: fromServices.DeliveryService
  ) {}

  @Effect()
  getDelivery$ = this.actions$.ofType(deliveryActions.DeliveryActionTypes.GetDelivery).pipe(
    switchMap(() => {
      return this.deliveryService
        .get()
        .pipe(
          map(delivery => new deliveryActions.GetDeliverySuccess(delivery)),
          catchError(error => of(new deliveryActions.GetDeliveryFail(error)))
        );
    })
  );

  @Effect()
  saveDelivery$ = this.actions$.ofType(deliveryActions.DeliveryActionTypes.SaveDelivery).pipe(
    map((action: deliveryActions.SaveDelivery) => action.payload),
    switchMap(delivery => {
      return this.deliveryService
        .saveDelivery(delivery)
        .pipe(
          switchMap(deliveryAddress => [
            new deliveryActions.SaveDeliverySuccess(deliveryAddress),
            new fromTotals.GetTotals(),
            new fromPickup.LoadPickupOptions(),
            new fromPickup.GetPickupOption(),
            new fromPickup.GetPreparationTime()
          ]),
          catchError(error => of(new deliveryActions.SaveDeliveryFail(error)))
        );
    })
  );

  @Effect()
  updateInstructions$ = this.actions$.ofType(deliveryActions.DeliveryActionTypes.UpdateInstructions).pipe(
    map((action: deliveryActions.UpdateInstructions) => action.payload),
    switchMap(delivery => {
      return this.deliveryService
        .updateInstructions(delivery)
        .pipe(
          switchMap(deliveryAddress => [
            new deliveryActions.UpdateInstructionsSuccess(deliveryAddress),
            new fromTotals.GetTotals(),
            new fromPickup.LoadPickupOptions(),
            new fromPickup.GetPickupOption(),
            new fromPickup.GetPreparationTime()
          ]),
          catchError(error => of(new deliveryActions.UpdateInstructionsFail(error)))
        );
    })
  );

  @Effect()
  clearDelivery$ = this.actions$.ofType(deliveryActions.DeliveryActionTypes.ClearDelivery).pipe(
    switchMap(() => {
      return this.deliveryService
        .clearDelivery()
        .pipe(
          switchMap(() => [
            new deliveryActions.ClearDeliverySuccess(),
            new fromTotals.GetTotals(),
            new fromPickup.LoadPickupOptions(),
            new fromPickup.GetPickupOption(),
            new fromPickup.GetPreparationTime()
          ]),
          catchError(error => of(new deliveryActions.ClearDeliveryFail(error)))
        );
    })
  );
}

import { Injectable } from '@angular/core';

import { Effect, Actions } from '@ngrx/effects';
import { map, switchMap, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

import * as deliveryActions from '../actions/delivery.action';
import * as fromServices from '../../services';

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
          map(delivery => new deliveryActions.SaveDeliverySuccess(delivery)),
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
          map(delivery => new deliveryActions.UpdateInstructionsSuccess(delivery)),
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
          map(() => new deliveryActions.ClearDeliverySuccess()),
          catchError(error => of(new deliveryActions.ClearDeliveryFail(error)))
        );
    })
  );
}
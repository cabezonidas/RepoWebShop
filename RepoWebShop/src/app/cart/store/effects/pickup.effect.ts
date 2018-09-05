import { Injectable } from '@angular/core';

import { Effect, Actions } from '@ngrx/effects';
import { map, switchMap, catchError, share, mergeMap, concatMap } from 'rxjs/operators';
import { of } from 'rxjs';

import * as pickupActions from '../actions/pickup.action';
import * as fromServices from '../../services';

@Injectable()
export class PickupEffects {
  constructor(
    private actions$: Actions,
    private pickupService: fromServices.PickupService
  ) {}

  @Effect()
  loadPickupOptions$ = this.actions$.ofType(pickupActions.PickupActionTypes.LoadPickupOptions).pipe(
    switchMap(() => {
      return this.pickupService
        .loadPickupOptions()
        .pipe(
          map(pickupOption => new pickupActions.LoadPickupOptionsSuccess(pickupOption)),
          catchError(error => of(new pickupActions.LoadPickupOptionsFail(error)))
        );
    }),
    share()
  );

  @Effect()
  setPickupOption$ = this.actions$.ofType(pickupActions.PickupActionTypes.SetPickupOption).pipe(
    map((action: pickupActions.SetPickupOption) => action.payload),
    concatMap(pickupId => {
      return this.pickupService
        .setPickupOption(pickupId)
        .pipe(
          switchMap(pickupDate => [
            new pickupActions.SetPickupOptionSuccess(pickupDate),
            new pickupActions.LoadPickupOptions(),
          ]),
          catchError(error => of(new pickupActions.SetPickupOptionFail(error)))
        );
    }),
    share()
  );

  @Effect()
  getPickupOption$ = this.actions$.ofType(pickupActions.PickupActionTypes.GetPickupOption).pipe(
    switchMap(() => {
      return this.pickupService
        .getPickupOption()
        .pipe(
          map(items => new pickupActions.GetPickupOptionSuccess(items)),
          catchError(error => of(new pickupActions.GetPickupOptionFail(error)))
        );
    })
  );

  @Effect()
  preparationTime$ = this.actions$.ofType(pickupActions.PickupActionTypes.GetPreparationTime).pipe(
    switchMap(() => {
      return this.pickupService
        .preparationTime()
        .pipe(
          map(hours => new pickupActions.GetPreparationTimeSuccess(hours)),
          catchError(error => of(new pickupActions.GetPreparationTimeFail(error)))
        );
    })
  );
}

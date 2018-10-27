import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { CateringService } from '../services/catering.service';
import * as cateringActions from './catering.actions';
import { mergeMap, map, catchError, tap, switchMap, share } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Action, Store } from '@ngrx/store';
import * as fromCatering from '.';
import { ICatering } from '../interfaces/ICatering';
import * as customCateringActions from '../../cart/store/actions/custom-catering.action';
import { CustomCateringService } from 'src/app/cart/services';
import * as fromTotals from '../../cart/store/actions/totals.action';
import * as fromPickup from '../../cart/store/actions/pickup.action';

@Injectable()
export class CateringEffects {
    constructor(private store: Store<fromCatering.State>,
        private cartService: CustomCateringService,
        private actions$: Actions, private cateringService: CateringService) { }

    @Effect()
    addItem$: Observable<Action> = this.actions$.ofType(cateringActions.CateringActionTypes.AddItem).pipe(
      map((action: cateringActions.AddItem) => action.payload),
      switchMap(itemId => {
          return this.cateringService
            .addItem(itemId)
            .pipe(
                switchMap(catering => [
                    new customCateringActions.LoadSessionCateringSuccess(catering),
                    new cateringActions.LoadSessionCateringDone()
                ]),
            catchError(err => of(new customCateringActions.LoadSessionCateringFail(err)))
            );
      }),
      share()
    );

    @Effect()
    removeItem$: Observable<Action> = this.actions$.ofType(cateringActions.CateringActionTypes.RemoveItem).pipe(
      map((action: cateringActions.RemoveItem) => action.payload),
      switchMap(itemId => {
          return this.cateringService
            .removeItem(itemId)
            .pipe(
                switchMap(catering => [
                    new customCateringActions.LoadSessionCateringSuccess(catering),
                    new cateringActions.LoadSessionCateringDone()
                ]),
            catchError(err => of(new customCateringActions.LoadSessionCateringFail(err)))
            );
      }),
      share()
    );

    @Effect()
    loadCaterings$: Observable<Action> = this.actions$.pipe(
        ofType(cateringActions.CateringActionTypes.LoadCaterings),
        mergeMap((action: cateringActions.LoadCaterings) => this.cateringService.loadCaterings().pipe(
            map((caterings: ICatering[]) => {
                return new cateringActions.LoadCateringsSuccess(caterings);
            }),
            catchError(err => of(new cateringActions.LoadCateringsFail(err)))
        ))
    );

    @Effect()
    loadCustomCatering$: Observable<Action> = this.actions$.pipe(
        ofType(cateringActions.CateringActionTypes.LoadCustomCatering),
        mergeMap((action: cateringActions.LoadCustomCatering) => this.cartService.loadSessionCatering().pipe(
            map((catering: ICatering) => new cateringActions.LoadCustomCateringSuccess(catering)),
            catchError(err => of(new cateringActions.LoadCustomCateringSuccess(err)))
        )),
        share()
    );

    @Effect()
    saveCustomCatering$: Observable<Action> = this.actions$.ofType(cateringActions.CateringActionTypes.SavingCustomCatering).pipe(
        map((action: cateringActions.SavingCustomCatering) => action.payload),
        switchMap(catItems => {
            return this.cateringService.saveLocalCatering(catItems)
                .pipe(
                    switchMap(items => [
                        new cateringActions.SavingCustomCateringSuccess(items),
                        new customCateringActions.LoadSessionCateringSuccess(items),
                        new fromTotals.GetTotals(),
                        new fromPickup.LoadPickupOptions(),
                        new fromPickup.GetPickupOption(),
                        new fromPickup.GetPreparationTime()
                    ]),
                catchError(err => of(new cateringActions.SavingCustomCateringFail(err)))
                );
            }),
        share()
    );
}

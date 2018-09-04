import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { CateringService } from '../services/catering.service';
import * as cateringActions from './catering.actions';
import { mergeMap, map, catchError, tap, switchMap, share } from 'rxjs/operators';
import { IItem } from '../../products/interfaces/iitem';
import { of, Observable } from 'rxjs';
import { Action, Store } from '@ngrx/store';
import * as fromCatering from '.';
import { ICatering } from '../interfaces/ICatering';
import * as customCateringActions from '../../cart/store/actions/custom-catering.action';

@Injectable()
export class CateringEffects {
    constructor(private store: Store<fromCatering.State>,
        private actions$: Actions, private cateringService: CateringService) { }

    @Effect()
    loadProducts$: Observable<Action> = this.actions$.pipe(
        ofType(cateringActions.CateringActionTypes.LoadItems),
        mergeMap((action: cateringActions.LoadItems) => this.cateringService.getItems().pipe(
            map((items: IItem[]) => {
                return new cateringActions.LoadItemsSuccess(items);
            }),
            catchError(err => of(new cateringActions.LoadItemsFail(err)))
        ))
    );

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
}

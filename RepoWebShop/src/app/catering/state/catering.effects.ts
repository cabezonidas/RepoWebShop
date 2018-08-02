import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { CateringService } from '../services/catering.service';
import * as cateringActions from './catering.actions';
import { mergeMap, map, catchError, tap } from 'rxjs/operators';
import { IItem } from '../../products/interfaces/iitem';
import { of, Observable } from 'rxjs';
import { Action, Store } from '@ngrx/store';
import * as fromCatering from '../state';

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
}
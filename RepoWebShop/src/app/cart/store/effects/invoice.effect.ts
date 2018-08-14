import { Injectable } from '@angular/core';

import { Effect, Actions } from '@ngrx/effects';
import { map, switchMap, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

import * as invoiceActions from '../actions/invoice.action';
import * as fromServices from '../../services';

@Injectable()
export class InvoicesEffects {
  constructor(
    private actions$: Actions,
    private invoiceService: fromServices.InvoiceService
  ) {}

  @Effect()
  isCuitValid$ = this.actions$.ofType(invoiceActions.InvoiceActionTypes.IsCuitValid).pipe(
    map((action: invoiceActions.IsCuitValid) => action.payload),
    switchMap(cuit => {
      return this.invoiceService
        .isCuitValid(cuit)
        .pipe(
          map(valid => new invoiceActions.IsCuitValidSuccess(valid)),
          catchError(error => of(new invoiceActions.IsCuitValidFail(error)))
        );
    })
  );  

  @Effect()
  addCuit$ = this.actions$.ofType(invoiceActions.InvoiceActionTypes.AddCuit).pipe(
    map((action: invoiceActions.AddCuit) => action.payload),
    switchMap(cuit => {
      return this.invoiceService
        .addCuit(cuit)
        .pipe(
          map(taxpayerInfo => new invoiceActions.AddCuitSuccess(taxpayerInfo)),
          catchError(error => of(new invoiceActions.AddCuitFail(error)))
        );
    })
  );

  @Effect()
  clearCuit$ = this.actions$.ofType(invoiceActions.InvoiceActionTypes.ClearCuit).pipe(
    switchMap(() => {
      return this.invoiceService
        .clearCuit()
        .pipe(
          map(() => new invoiceActions.ClearCuitSuccess()),
          catchError(error => of(new invoiceActions.ClearCuitFail(error)))
        );
    })
  );
}
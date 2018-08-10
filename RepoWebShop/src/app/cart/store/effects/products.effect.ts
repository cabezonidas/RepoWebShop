import { Injectable } from '@angular/core';

import { Effect, Actions } from '@ngrx/effects';
import { map, switchMap, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

import * as productActions from '../actions/products.action';
import * as fromServices from '../../services';

@Injectable()
export class ProductsEffects {
  constructor(
    private actions$: Actions,
    private productService: fromServices.ProductsService
  ) {}

  @Effect()
  loadPizzas$ = this.actions$.ofType(productActions.ActionTypes.Load).pipe(
    switchMap(() => {
      return this.productService
        .getCartItems()
        .pipe(
          map(items => new productActions.LoadProductsSuccess(items)),
          catchError(error => of(new productActions.LoadProductsFail(error)))
        );
    })
  );

  @Effect()
  addProduct$ = this.actions$.ofType(productActions.ActionTypes.Add).pipe(
    map((action: productActions.AddProduct) => action.payload),
    switchMap(productId => {
      return this.productService
        .addProduct(productId)
        .pipe(
          map(items => new productActions.AddProductSuccess(items)),
          catchError(error => of(new productActions.AddProductFail(error)))
        );
    })
  );

  @Effect()
  removeProduct$ = this.actions$.ofType(productActions.ActionTypes.Remove).pipe(
    map((action: productActions.RemoveProduct) => action.payload),
    switchMap(productId => {
      return this.productService
        .removeProduct(productId)
        .pipe(
          map(items => new productActions.RemoveProductSuccess(items)),
          catchError(error => of(new productActions.RemoveProductFail(error)))
        );
    })
  );

//   @Effect()
//   handlePizzaSuccess$ = this.actions$
//     .ofType(
//       pizzaActions.UPDATE_PIZZA_SUCCESS,
//       pizzaActions.REMOVE_PIZZA_SUCCESS
//     )
//     .pipe(
//       map(pizza => {
//         return new fromRoot.Go({
//           path: ['/products'],
//         });
//       })
//     );
}
import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { ProductsService } from '../services/products.service';
import * as productActions from './product.actions';
import { mergeMap, map, catchError } from 'rxjs/operators';
import { IProduct } from '../interfaces/iproduct';
import { of, Observable } from 'rxjs';
import { Action } from '@ngrx/store';

@Injectable()
export class ProductEffects {
    constructor(private actions$: Actions, private productService: ProductsService) { }

    @Effect()
    loadProducts$: Observable<Action> = this.actions$.pipe(
        ofType(productActions.ProductActionTypes.Load),
        mergeMap((action: productActions.Load) => this.productService.getProducts().pipe(
            map((products: IProduct[]) => (new productActions.LoadSuccess(products))),
            catchError(err => of(new productActions.LoadFail(err)))
        ))
    );

    // @Effect()
    // updateProduct$: Observable<Action> = this.actions$.pipe(
    //     ofType(productActions.ProductActionTypes.UpdateProduct),
    //     map((action: productActions.UpdateProduct) => action.payload),
    //     mergeMap((product: IProduct) =>  // Este mergeMap es para no anidar dos observables, el del action, y el del service
    //         this.productService.updateProduct(product).pipe(
    //             map(updateProduct => (new productActions.UpdateProductSuccess(updateProduct))),
    //             catchError(err => of(new productActions.UpdateProductFail(err)))
    //         )
    //     )
    // );
}

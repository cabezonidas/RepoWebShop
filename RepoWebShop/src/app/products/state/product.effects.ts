import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { ProductsService } from '../services/products.service';
import * as productActions from './product.actions';
import { mergeMap, map, catchError, tap } from 'rxjs/operators';
import { IProduct } from '../interfaces/iproduct';
import { of, Observable } from 'rxjs';
import { Action, Store } from '@ngrx/store';
import { ImagesService } from '../services/images.service';

import * as fromProduct from '.';
import { IAlbum } from '../interfaces/ialbum';

@Injectable()
export class ProductEffects {
    constructor(private store: Store<fromProduct.State>,
        private actions$: Actions, private productService: ProductsService, private albumService: ImagesService) { }

    @Effect()
    loadProducts$: Observable<Action> = this.actions$.pipe(
        ofType(productActions.ProductActionTypes.LoadProducts),
        mergeMap((action: productActions.LoadProducts) => this.productService.getProducts().pipe(
            map((products: IProduct[]) => {
                products.forEach(product => {
                    if (product.flickrAlbumId) {
                        this.store.dispatch(new productActions.LoadAlbum(product.flickrAlbumId));
                    }
                });
                return new productActions.LoadProductsSuccess(products);
            }),
            catchError(err => of(new productActions.LoadProductsFail(err)))
        ))
    );

    @Effect()
    loadAlbum$: Observable<Action> = this.actions$.pipe(
        ofType(productActions.ProductActionTypes.LoadAlbum),
        map((action: productActions.LoadAlbum) => action.payload),
        mergeMap((albumId: string) =>  // Este mergeMap es para no anidar dos observables, el del action, y el del service
            this.albumService.getAlbum(albumId).pipe(
                map(albumSuccess => {
                    albumSuccess.albumId = albumId;
                    return (new productActions.LoadAlbumSuccess(albumSuccess));
                }),
                catchError(err => of(new productActions.LoadAlbumFail(err)))
            )
        )
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

import { Component, OnInit, OnDestroy } from '@angular/core';
import { ProductsService } from '../../services/products.service';
import { IProduct } from '../../interfaces/iproduct';
import { AuthService } from '../../../authentication/services/auth.service';
import { Store, select } from '@ngrx/store';
import * as fromProduct from '../../state/product.reducer';
import * as productActions from '../../state/product.action';
import { takeWhile, tap, map, filter } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-products',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss']
})
export class GridComponent implements OnInit, OnDestroy {

  products$: Observable<IProduct[]>;
  errorMessage$: Observable<string>;
  productsPreviews$: Observable<IProduct[]>;
  componentActive = true;

  constructor(private store: Store<fromProduct.State>, private productsService: ProductsService, private auth: AuthService) { }

  ngOnInit() {
    // this.products.getProducts().subscribe(
    //   products => {
    //     this.products$ = products;
    //     this.productsPreview$ = products.filter(x => x.flickrAlbumId);
    //   }
    // );
    // TODO: Unsubscribe
    this.store.dispatch(new productActions.Load());

    // this.store.pipe(
    //   select(fromProduct.getProducts),
    //   takeWhile(() => this.componentActive),
    //   tap((prods) => console.log(prods))
    // ).subscribe((products: IProduct[]) => this.products = products);

    this.products$ = this.store.pipe(select(fromProduct.getProducts));
    // this.productsPreviews$ = this.store.pipe(
    //   select(fromProduct.getProducts),
    //   filter((product: IProduct) => !!product.flickrAlbumId)
    // );
    this.errorMessage$ = this.store.pipe(select(fromProduct.getError));
  }


  checkChanged(value: boolean): void {
    this.store.dispatch(
      new productActions.ToggleProductCode(true)
    );
  }

  ngOnDestroy() {
    // this.componentActive = false;
  }
}



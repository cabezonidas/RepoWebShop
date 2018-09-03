import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';

import * as fromProduct from '../../state';
import * as productActions from '../../state/product.actions';
import { IProduct } from '../../interfaces/iproduct';
import { filter, map, tap } from 'rxjs/operators';

@Component({
  templateUrl: './product-shell.component.html',
  styleUrls: ['./product-shell.component.scss']
  // changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductShellComponent implements OnInit {
  products$: Observable<IProduct[]>;
  items$: Observable<IProduct[]>;
  errorMessage$: Observable<string>;

  constructor(private store: Store<fromProduct.State>) {}

  ngOnInit(): void {
    this.store.dispatch(new productActions.LoadProducts());
    this.products$ = this.store.pipe(select(fromProduct.getProducts));
    this.items$ = this.store.pipe(
      select(fromProduct.getProducts),
      map(a => a.filter(x => x.isActive))
    );

    this.errorMessage$ = this.store.pipe(select(fromProduct.getError));
  }
}

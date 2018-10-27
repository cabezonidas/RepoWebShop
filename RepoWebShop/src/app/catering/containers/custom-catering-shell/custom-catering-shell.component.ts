import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IItem } from 'src/app/products/interfaces/iitem';
import { Store, select } from '@ngrx/store';

import * as fromProducts from '../../../products/state';
import * as fromCatering from '../../state';
import * as cateringActions from '../../state/catering.actions';
import { map, mergeMap, groupBy, tap, switchMap } from 'rxjs/operators';
import { Title } from '@angular/platform-browser';
import { ProductsService } from '../../../products/services/products.service';
import { ICateringItem } from '../../interfaces/ICateringItem';
import { CateringService } from '../../services/catering.service';

@Component({
  selector: 'app-custom-catering-shell',
  templateUrl: './custom-catering-shell.component.html',
  styleUrls: ['./custom-catering-shell.component.scss']
})
export class CustomCateringShellComponent implements OnInit {
  items$: Observable<IItem[]>;
  selectedItems$: Observable<ICateringItem[]>;
  price$: Observable<number>;
  priceInStore$: Observable<number>;

  constructor(private cateringStore: Store<fromCatering.State>, private productsStore: Store<fromProducts.State>,
    private titleService: Title, private productsServ: ProductsService, private catService: CateringService) { }

  ngOnInit() {
    this.titleService.setTitle('Catering personalizado');

    this.items$ = this.productsStore.pipe(
      select(fromProducts.getProducts),
      map(prods => {
        const items: IItem[] = [];
        prods.forEach(prod => {
          prod.items.forEach(item => items.push(item));
        });
        return items.sort((a, b) => this.productsServ.reverseCompare(a, b));
      })
    );

    this.selectedItems$ = this.cateringStore.pipe(
      select(fromCatering.getSelectedItems),
      map(selected => this.catService.groupItems(selected))
    );

    this.price$ = this.selectedItems$.pipe(
      map(items => items.map(i => i.item.price * i.itemCount).reduce((a, b) => a + b, 0))
    );

    this.priceInStore$ = this.selectedItems$.pipe(
      map(items => items.map(i => i.item.priceInStore * i.itemCount).reduce((a, b) => a + b, 0))
    );
  }
  addItem = (item: IItem) => {
    this.cateringStore.dispatch(new cateringActions.AddLocalItem(item));
  }
  removeItem = (item: IItem) => {
    this.cateringStore.dispatch(new cateringActions.RemoveLocalItem(item));
  }
}

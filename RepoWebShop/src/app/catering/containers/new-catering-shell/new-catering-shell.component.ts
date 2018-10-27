import { Component, OnInit, HostBinding, OnChanges } from '@angular/core';
import { IItem } from '../../../products/interfaces/iitem';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';

import * as fromProducts from '../../../products/state';
import * as fromCatering from '../../state';
import * as cateringActions from '../../state/catering.actions';
import { map, tap } from 'rxjs/operators';
import { ICatering } from '../../interfaces/ICatering';
import { moveIn } from '../../../animations/router.animations';
import * as fromStore from '../../../cart/store';
import { CateringService } from '../../services/catering.service';
import { Title } from '@angular/platform-browser';
import { ProductsService } from '../../../products/services/products.service';

@Component({
  selector: 'app-new-catering-shell',
  templateUrl: './new-catering-shell.component.html',
  styleUrls: ['./new-catering-shell.component.scss'],
  animations: [moveIn()]
})
export class NewCateringShellComponent implements OnInit {
  items$: Observable<IItem[]>;
  catering$: Observable<ICatering>;
  loading$: Observable<boolean>;
  errorMessage$: Observable<string>;

  constructor(private cateringStore: Store<fromCatering.State>, private productsStore: Store<fromProducts.State>,
    private titleService: Title, private productsServ: ProductsService) { }
  @HostBinding('@moveIn') role = '';

  ngOnInit() {
    this.titleService.setTitle('Catering personalizado');

    this.catering$ = this.cateringStore.pipe(select(fromStore.getCustomCatering));

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

    this.loading$ = this.cateringStore.pipe(
      select(fromCatering.getLoading)
      );

    this.errorMessage$ = this.cateringStore.pipe(select(fromCatering.getError));
  }

  addItem(item: IItem): void {
    this.cateringStore.dispatch(new cateringActions.AddItem(item.productId));
  }

  removeItem(itemId: number): void {
    this.cateringStore.dispatch(new cateringActions.RemoveItem(itemId));
  }

}


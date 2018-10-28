import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subscription, Subject } from 'rxjs';
import { IItem } from 'src/app/products/interfaces/iitem';
import { Store, select } from '@ngrx/store';
import * as fromProducts from '../../../products/state';
import * as fromCatering from '../../state';
import * as cateringActions from '../../state/catering.actions';
import { map, mergeMap, switchMap, filter, take, concatMap, tap } from 'rxjs/operators';
import { Title } from '@angular/platform-browser';
import { ProductsService } from '../../../products/services/products.service';
import { ICateringItem } from '../../interfaces/ICateringItem';
import { CateringService } from '../../services/catering.service';
import * as fromStore from '../../state';

@Component({
  selector: 'app-custom-catering-shell',
  templateUrl: './custom-catering-shell.component.html',
  styleUrls: ['./custom-catering-shell.component.scss']
})
export class CustomCateringShellComponent implements OnInit, OnDestroy {
  items$: Observable<IItem[]>;
  selectedItems$: Observable<ICateringItem[]>;
  price$: Observable<number>;
  priceInStore$: Observable<number>;
  preparationTime$: Observable<number>;
  saveSub = new Subscription();
  saving = new Subject<boolean>();
  savingButton$ = this.saving.asObservable();
  saving$: Observable<boolean>;

  constructor(private cateringStore: Store<fromCatering.State>, private productsStore: Store<fromProducts.State>,
    private titleService: Title, private productsServ: ProductsService, private catService: CateringService,
    private store: Store<fromStore.State>) { }

  ngOnInit() {
    this.store.dispatch(new cateringActions.LoadCustomCatering());
    this.saving$ = this.cateringStore.pipe(select(fromCatering.getSavingCustomCatering));
    this.titleService.setTitle('Catering personalizado');

    this.items$ = this.productsStore.pipe(
      select(fromProducts.getProducts),
      map(prods => {
        const items: IItem[] = [];
        prods.forEach(prod => prod.items.forEach(item => items.push(item)));
        prods.map(p => p.items).concat();
        return items.sort((a, b) => this.productsServ.reverseCompare(a, b));
      })
    );

    this.saveSub = this.savingButton$.pipe(
      filter(saving => saving),
      switchMap(_ => this.selectedItems$.pipe(
        take(1),
        mergeMap(selectedItems => [this.store.dispatch(new cateringActions.SavingCustomCatering(selectedItems))])
      )
    )).subscribe();

    this.selectedItems$ = this.cateringStore.pipe(
      select(fromCatering.getSelectedItems),
      map(selected => this.catService.groupItems(selected))
    );

    this.preparationTime$ = this.selectedItems$.pipe(
      filter(selectedItems => selectedItems && selectedItems.length > 0),
      map(selectedItems => selectedItems.sort((a, b) => a.item.preparationTime < b.item.preparationTime ? 1 : -1)[0]),
      concatMap(catItem => this.catService.getCustomCateringPreptime(catItem.item.preparationTime))
    );

    this.price$ = this.selectedItems$.pipe(map(items => items.map(i => i.item.price * i.itemCount).reduce((a, b) => a + b, 0)));
    this.priceInStore$ = this.selectedItems$
      .pipe(map(items => items.map(i => i.item.priceInStore * i.itemCount).reduce((a, b) => a + b, 0)));

  }
  addItem = (item: IItem) => this.cateringStore.dispatch(new cateringActions.AddLocalItem(item));
  removeItem = (item: IItem) =>  this.cateringStore.dispatch(new cateringActions.RemoveLocalItem(item));
  ngOnDestroy = () => {
    this.saveSub.unsubscribe();
  }
}

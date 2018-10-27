import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { Observable, Subscription, of, Subject } from 'rxjs';
import { IItem } from 'src/app/products/interfaces/iitem';
import { Store, select } from '@ngrx/store';
import * as fromProducts from '../../../products/state';
import * as fromCatering from '../../state';
import * as fromCateringEffects from '../../state/catering.effects';
import * as cateringActions from '../../state/catering.actions';
import { map, mergeMap, groupBy, tap, switchMap, throttleTime, filter, share, take } from 'rxjs/operators';
import { Title } from '@angular/platform-browser';
import { ProductsService } from '../../../products/services/products.service';
import { ICateringItem } from '../../interfaces/ICateringItem';
import { CateringService } from '../../services/catering.service';
import * as fromStore from '../../state';
import { ICatering } from '../../interfaces/ICatering';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material';

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
  @ViewChild('saveCat') saveCatDiv: ElementRef;
  saveSub = new Subscription();
  onceSaved = new Subscription();
  customCatering$: Observable<ICatering>;
  customCateringLoading$: Observable<boolean>;
  saving$: Observable<boolean>;

  constructor(private cateringStore: Store<fromCatering.State>, private productsStore: Store<fromProducts.State>,
    private titleService: Title, private productsServ: ProductsService, private catService: CateringService,
    private store: Store<fromStore.State>, private router: Router, private cateringEffects: fromCateringEffects.CateringEffects) { }

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

    this.selectedItems$ = this.cateringStore.pipe(
      select(fromCatering.getSelectedItems),
      map(selected => this.catService.groupItems(selected))
    );

    this.price$ = this.selectedItems$.pipe(map(items => items.map(i => i.item.price * i.itemCount).reduce((a, b) => a + b, 0)));
    this.priceInStore$ = this.selectedItems$
      .pipe(map(items => items.map(i => i.item.priceInStore * i.itemCount).reduce((a, b) => a + b, 0)));

    // this.onceSaved = this.cateringEffects.saveCustomCatering$
    //   .pipe(filter(action => action.type === cateringActions.CateringActionTypes.SavingCustomCateringSuccess))
    //   .subscribe(() => this.router.navigateByUrl('/cart'));
  }
  addItem = (item: IItem) => this.cateringStore.dispatch(new cateringActions.AddLocalItem(item));
  removeItem = (item: IItem) =>  this.cateringStore.dispatch(new cateringActions.RemoveLocalItem(item));
  ngOnDestroy = () => {
    this.saveSub.unsubscribe();
    this.onceSaved.unsubscribe();
  }

  saveCatering = () => {
    this.saveSub = this.selectedItems$.pipe(
      take(1),
      mergeMap(selectedItems => [this.store.dispatch(new cateringActions.SavingCustomCatering(selectedItems))])
    ).subscribe();
  }
}

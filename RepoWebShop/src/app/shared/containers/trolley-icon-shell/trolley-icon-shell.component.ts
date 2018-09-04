import { Component, OnInit } from '@angular/core';
import * as fromEffects from '../../../cart/store/effects';
import * as fromStore from '../../../cart/store';
import { Observable } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { ICartCatalogItem } from '../../../cart/interfaces/icart-catalog-item';
import { ICartCatering } from '../../../cart/interfaces/icart-catering';
import { ICatering } from '../../../catering/interfaces/ICatering';

@Component({
  selector: 'app-trolley-icon-shell',
  templateUrl: './trolley-icon-shell.component.html',
  styleUrls: ['./trolley-icon-shell.component.scss']
})
export class TrolleyIconShellComponent implements OnInit {

  items$: Observable<ICartCatalogItem[]>;
  caterings$: Observable<ICartCatering[]>;
  customCatering$: Observable<ICatering>;
  constructor(
    private store: Store<fromStore.CartState>
  ) { }

  ngOnInit() {
    this.store.dispatch(new fromStore.LoadCaterings());
    this.store.dispatch(new fromStore.LoadSessionCatering());
    this.store.dispatch(new fromStore.LoadItems());
    this.items$ = this.store.pipe(select(fromStore.getItems));
    this.caterings$ = this.store.pipe(select(fromStore.getCaterings));
    this.customCatering$ = this.store.pipe(select(fromStore.getCustomCatering));
  }

}

import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';

import * as fromProduct from '../../state';
import * as productActions from '../../state/product.actions';
import { IProduct } from '../../interfaces/iproduct';
import { map } from 'rxjs/operators';
import { CalendarService } from '../../../home/services/calendar.service';
import { ScrollService } from '../../../home/services/scroll.service';

@Component({
  selector: 'app-full-catalog-shell',
  templateUrl: './full-catalog-shell.component.html',
  styleUrls: ['./full-catalog-shell.component.scss']
})
export class FullCatalogShellComponent implements OnInit, AfterViewInit {
  products$: Observable<IProduct[]>;
  errorMessage$: Observable<string>;

  constructor(private store: Store<fromProduct.State>, private calendar: CalendarService, private scroll: ScrollService) {}

  ngOnInit() {
    // this.store.dispatch(new productActions.LoadProducts());
    this.products$ = this.store.pipe(select(fromProduct.getProducts));
    this.scroll.instantScrollTo('top', -64);
  }

  ngAfterViewInit() {
  }
}

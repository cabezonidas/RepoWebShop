import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';

import * as fromProduct from '../../state';
import * as productActions from '../../state/product.actions';
import { IProduct } from '../../interfaces/iproduct';
import { map } from 'rxjs/operators';
import { CalendarService } from '../../../home/services/calendar.service';
import { Title } from '@angular/platform-browser';
import { ScrollService } from '../../../home/services/scroll.service';
@Component({
  templateUrl: './product-shell.component.html',
  styleUrls: ['./product-shell.component.scss']
  // changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductShellComponent implements OnInit {
  items$: Observable<IProduct[]>;
  errorMessage$: Observable<string>;

  constructor(private store: Store<fromProduct.State>, private calendar: CalendarService,
    private titleService: Title, private scroll: ScrollService) {}

  ngOnInit(): void {
    this.titleService.setTitle('Productos destacados');
    // this.store.dispatch(new productActions.LoadProducts());
    this.items$ = this.store.pipe(
      select(fromProduct.getProducts),
      map(a => a.filter(x => x.isActive))
    );

    this.errorMessage$ = this.store.pipe(select(fromProduct.getError));
    this.scroll.instantScrollTo('top', -64);
  }
}

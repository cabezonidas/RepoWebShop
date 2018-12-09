import { Component, OnInit } from '@angular/core';
import * as fromProducts from '../../state';
import { Store, select } from '@ngrx/store';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { IProduct } from '../../interfaces/iproduct';

@Component({
  selector: 'app-highlighted-carousel-shell',
  templateUrl: './highlighted-carousel-shell.component.html',
  styleUrls: ['./highlighted-carousel-shell.component.scss']
})
export class HighlightedCarouselShellComponent implements OnInit {

  piesOfTheWeek$: Observable<IProduct[]>;
  constructor(
    private store: Store<fromProducts.State>
  ) { }

  ngOnInit() {
    this.piesOfTheWeek$ = this.store.pipe(
      select(fromProducts.getProducts),
      map(products => products.filter(product => product.isPieOfTheWeek).slice(0, 3))
    );
  }
}

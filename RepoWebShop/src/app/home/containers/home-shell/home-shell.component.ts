import { Component, OnInit } from '@angular/core';
import { ScrollService } from '../../services/scroll.service';
import { Title } from '@angular/platform-browser';
import { Store } from '@ngrx/store';
import * as fromProduct from '../../../products/state';
import * as productActions from '../../../products/state/product.actions';
import * as cateringActions from '../../../catering/state/catering.actions';

@Component({
  selector: 'app-home-shell',
  templateUrl: './home-shell.component.html',
  styleUrls: ['./home-shell.component.scss']
})
export class HomeShellComponent implements OnInit {

  constructor(private scroll: ScrollService, private titleService: Title, private store: Store<fromProduct.State>) { }

  ngOnInit() {
    this.titleService.setTitle('De las Artes');

    // this.store.dispatch(new productActions.LoadProducts());
    // this.store.dispatch(new cateringActions.LoadCaterings());
    // this.store.dispatch(new cateringActions.LoadItems());
  }
  triggerScrollTo(target: string, offset: number) {
    this.scroll.triggerScrollTo(target, offset);
  }
}

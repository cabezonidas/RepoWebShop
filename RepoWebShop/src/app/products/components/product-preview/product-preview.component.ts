import { Component, Input, HostBinding, OnInit, OnDestroy } from '@angular/core';
import { MatBottomSheet } from '@angular/material';
import { ChooseItemComponent } from '../choose-item/choose-item.component';
import { IProduct } from '../../interfaces/iproduct';
import { IItem } from '../../interfaces/iitem';
import { Store, select } from '@ngrx/store';
import * as productActions from '../../state/product.actions';
import * as fromProduct from '../../state';
import { filter, map } from 'rxjs/operators';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-product-preview',
  templateUrl: './product-preview.component.html',
  styleUrls: ['./product-preview.component.scss']
})
export class ProductPreviewComponent implements OnInit, OnDestroy {
  @Input() product: IProduct;
  albumSub = new Subscription();
  loadDispatch = false;

  constructor(private bottomSheet: MatBottomSheet, private store: Store<fromProduct.State>) {}

  ngOnInit() {
    this.albumSub = this.store.pipe(
      select(fromProduct.getAlbums),
      map(albums => albums.map(e => e.albumId).indexOf(this.product.flickrAlbumId) !== -1)
    ).subscribe(albumFound => {
      if (!albumFound && !this.loadDispatch) {
        this.loadDispatch = true;
        this.store.dispatch(new productActions.LoadAlbum(this.product.flickrAlbumId));
      }
    });
  }

  ngOnDestroy(): void {
    this.albumSub.unsubscribe();
  }

  chooseProduct(): void {
    this.bottomSheet.open(ChooseItemComponent, {
      data: this.product.items as Array<IItem>
    });
  }
}

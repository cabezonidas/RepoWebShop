import { Component, Input, HostBinding, OnInit } from '@angular/core';
import { MatBottomSheet } from '@angular/material';
import { ChooseItemComponent } from '../choose-item/choose-item.component';
import { IProduct } from '../../interfaces/iproduct';
import { IItem } from '../../interfaces/iitem';
import { Store } from '@ngrx/store';
import * as productActions from '../../state/product.actions';
import * as fromProduct from '../../state';

@Component({
  selector: 'app-product-preview',
  templateUrl: './product-preview.component.html',
  styleUrls: ['./product-preview.component.scss']
})
export class ProductPreviewComponent implements OnInit {

  @Input() product: IProduct;

  constructor(private bottomSheet: MatBottomSheet, private store: Store<fromProduct.State>) {}

  ngOnInit() {
    if (this.product.flickrAlbumId && (!this.product.album || !this.product.album.photos || !this.product.album.photos.length)) {
      this.store.dispatch(new productActions.LoadAlbum(this.product.flickrAlbumId));
    }
  }

  chooseProduct(): void {
    this.bottomSheet.open(ChooseItemComponent, {
      data: this.product.items as Array<IItem>
    });
  }
}

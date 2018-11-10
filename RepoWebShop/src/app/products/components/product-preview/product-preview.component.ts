import { Component, Input, HostBinding, OnInit, OnDestroy } from '@angular/core';
import { MatBottomSheet } from '@angular/material';
import { ChooseItemComponent } from '../choose-item/choose-item.component';
import { IProduct } from '../../interfaces/iproduct';
import { IItem } from '../../interfaces/iitem';
import { Store, select } from '@ngrx/store';
import * as productActions from '../../state/product.actions';
import * as fromProduct from '../../state';
import { filter, map, tap, take } from 'rxjs/operators';
import { Subscription, Observable } from 'rxjs';
import { IAlbum } from '../../interfaces/ialbum';
import { ImagesService } from '../../services/images.service';

@Component({
  selector: 'app-product-preview',
  templateUrl: './product-preview.component.html',
  styleUrls: ['./product-preview.component.scss']
})
export class ProductPreviewComponent implements OnInit, OnDestroy {
  @Input() product: IProduct;
  album$: Observable<string[]>;
  albumSub = new Subscription();
  loadDispatch = false;

  constructor(private bottomSheet: MatBottomSheet, private store: Store<fromProduct.State>, private images: ImagesService) {}

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

    this.album$ = this.store.pipe(
      select(fromProduct.getAlbums),
      map(albums => albums.find(album => album.albumId === this.product.flickrAlbumId)),
      filter(album => !!album),
      take(1),
      map(album => album.photos.map(img => this.images.smallUrl_240(img)))
    );
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

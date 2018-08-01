import { Component, OnInit, Input, AfterViewChecked, OnDestroy } from '@angular/core';
import { IAlbum } from '../../interfaces/ialbum';
import { ImagesService } from '../../services/images.service';
import * as MobileDetect from 'mobile-detect/mobile-detect';
import * as M from 'materialize-css';
import { Store } from '@ngrx/store';
import * as fromProduct from '../../state';
import { Observable, Subscription } from 'rxjs';
import { select } from '@ngrx/store';
import { first, map } from '../../../../../node_modules/rxjs/operators';
import { IProduct } from '../../interfaces/iproduct';

@Component({
  selector: 'app-product-carousel-preview',
  templateUrl: './product-carousel-preview.component.html',
  styleUrls: ['./product-carousel-preview.component.scss']
})


export class ProductCarouselPreviewComponent implements OnInit, AfterViewChecked, OnDestroy {

  @Input() albumId: string;

  constructor(private images: ImagesService, private store: Store<fromProduct.State>) { }

  albumsSub = new Subscription();
  currentAlbum: IAlbum;
  albumInit = false;
  thumbnails: Array<string> = [];
  mobileDetect = new MobileDetect(window.navigator.userAgent);

  ngAfterViewChecked(): void {
    if (this.currentAlbum && !this.albumInit) {
      this.albumInit = true;
      M.Carousel.init(document.getElementById(this.albumId), { });
    }
  }

  ngOnInit() {
    this.albumsSub = this.store.pipe(
      select(fromProduct.getAlbums),
      map(albums => albums.find(album => album.albumId === this.albumId))
    ).subscribe(album => {
      if (album) {
        this.currentAlbum = album;
        this.currentAlbum.photos.forEach(x => this.thumbnails.push(this.images.smallUrl_240(x)));
        this.albumsSub.unsubscribe();
      }
    });
  }

  ngOnDestroy() {
    this.albumsSub.unsubscribe();
  }
}

import { Component, OnInit, Input, AfterViewChecked, OnDestroy, ViewChild, ElementRef, Output, EventEmitter } from '@angular/core';
import * as MobileDetect from 'mobile-detect/mobile-detect';
import { Store } from '@ngrx/store';
import * as fromProduct from '../../state';
import { Observable, Subscription } from 'rxjs';
import { select } from '@ngrx/store';
import { first, map } from 'rxjs/operators';
import { ImagesService } from 'src/app/products/services/images.service';
import { IAlbum } from 'src/app/products/interfaces/ialbum';

@Component({
  selector: 'app-caterings-carousel-shell',
  templateUrl: './caterings-carousel-shell.component.html',
  styleUrls: ['./caterings-carousel-shell.component.scss']
})
export class CateringsCarouselShellComponent implements OnInit, AfterViewChecked, OnDestroy {
  M: any;
  videoWidth = 560;
  videoHeight = 315;
  @ViewChild('video') public videoElement: ElementRef;
  @Output() viewCaterings = new EventEmitter<void>();

  constructor(private images: ImagesService, private store: Store<fromProduct.State>) {
    if (typeof window !== 'undefined') {
      this.M = require('materialize-css');
    }
  }
  albumId = '72157693586449552';
  itemId = 'caterings-preview';
  albumsSub = new Subscription();
  currentAlbum: IAlbum;
  albumInit = false;
  thumbnails: Array<string> = [];
  mobileDetect = new MobileDetect(window.navigator.userAgent);
  ngAfterViewChecked(): void {
    if (this.currentAlbum && !this.albumInit) {
      this.albumInit = true;
      this.M.Carousel.init(document.getElementById(this.albumId + '-' + this.itemId), { });
    }
  }

  onResize(event) {
    this.videoWidth = this.videoElement.nativeElement.offsetWidth;
    this.videoHeight = this.videoWidth * 315 / 560;
  }

  ngOnInit() {
    this.onResize(null);
    this.albumsSub = this.images.getAlbum(this.albumId)
    .subscribe(album => {
      if (album) {
        this.currentAlbum = album;
        this.currentAlbum.photos.forEach(x => this.thumbnails.push(this.images.med_640(x)));
        this.albumsSub.unsubscribe();
      }
    });
  }

  ngOnDestroy() {
    this.albumsSub.unsubscribe();
  }
}

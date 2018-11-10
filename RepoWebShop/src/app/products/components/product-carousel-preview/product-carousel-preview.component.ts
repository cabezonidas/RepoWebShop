import { Component, Input, AfterViewChecked, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-product-carousel-preview',
  templateUrl: './product-carousel-preview.component.html',
  styleUrls: ['./product-carousel-preview.component.scss']
})


export class ProductCarouselPreviewComponent implements AfterViewChecked {

  @Input() thumbnails: Array<string>;
  @ViewChild('carousel') carousel: ElementRef;
  M: any;

  constructor() {
    if (typeof window !== 'undefined') {
      this.M = require('materialize-css');
    }
  }

  albumInit = false;

  ngAfterViewChecked(): void {
    if (this.thumbnails && this.thumbnails.length && !this.albumInit) {
      this.albumInit = true;
      this.M.Carousel.init(this.carousel.nativeElement, { });
    }
  }
}

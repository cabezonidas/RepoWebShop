import { Component, OnInit, Input, AfterViewChecked } from '@angular/core';
import { IAlbum } from '../../../interfaces/ialbum';
import { ImagesService } from '../../../services/images.service';
import * as MobileDetect from 'mobile-detect/mobile-detect';
import * as M from 'materialize-css';

@Component({
  selector: 'app-product-carousel-preview',
  templateUrl: './product-carousel-preview.component.html',
  styleUrls: ['./product-carousel-preview.component.scss']
})


export class ProductCarouselPreviewComponent implements OnInit, AfterViewChecked {

  @Input() albumId: string;

  constructor(private images: ImagesService) { }

  album$: IAlbum;
  thumbnails: Array<string> = [];
  largePics: Array<string> = [];
  initCarouse = false;
  options = { };

  ngAfterViewChecked(): void {
    if (this.album$ && !this.initCarouse) {
      this.initCarouse = true;
      const elems = document.getElementById(this.albumId);
      M.Carousel.init(elems, this.options);
    }
  }

  ngOnInit() {
    this.images.getAlbum(this.albumId).subscribe(album => {
      this.album$ = album;
      const md = new MobileDetect(window.navigator.userAgent);
      album.photos.forEach(x => {
        this.thumbnails.push(this.images.smallUrl_240(x));
        if (md.isPhoneSized) {
          this.largePics.push(this.images.med_640(x));
        } else {
          this.largePics.push(this.images.large_1024(x));
        }
      });
    });
  }
}

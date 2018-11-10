import { Component, OnInit, ViewChild, ElementRef, Output, EventEmitter } from '@angular/core';
import { ImagesService } from 'src/app/products/services/images.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-caterings-carousel-shell',
  templateUrl: './caterings-carousel-shell.component.html',
  styleUrls: ['./caterings-carousel-shell.component.scss']
})
export class CateringsCarouselShellComponent implements OnInit {
  videoWidth = 560;
  videoHeight = 315;
  @ViewChild('video') public videoElement: ElementRef;
  @Output() viewCaterings = new EventEmitter<void>();

  constructor(private images: ImagesService) { }
  thumbnails$: Observable<string[]>;

  onResize(event) {
    this.videoWidth = this.videoElement.nativeElement.offsetWidth;
    this.videoHeight = this.videoWidth * 315 / 560;
  }

  ngOnInit() {
    this.onResize(null);
    this.thumbnails$ = this.images.getAlbum('72157693586449552').pipe(
      map(album => album.photos.map(img => this.images.med_640(img)))
    );
  }
}

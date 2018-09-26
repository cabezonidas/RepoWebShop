import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-video',
  templateUrl: './video.component.html',
  styleUrls: ['./video.component.scss']
})
export class VideoComponent implements OnInit {

  videoSource: string;
  videoHeight: number;
  @ViewChild('video') public videoElement: ElementRef;
  constructor() { }

  ngOnInit() {
    this.onResize(null);
  }
  onResize(event) {
    this.videoHeight = this.videoElement.nativeElement.offsetHeight;
    this.videoSource = this.chooseVideo();
  }

  chooseVideo() {
    let size = '320';
    if (window.innerWidth > 0 && window.innerWidth < 540) {
      size = '320';
    }
    if (window.innerWidth >= 540 && window.innerWidth < 720) {
      size = '540';
    }
    if (window.innerWidth >= 720) {
      size = '720';
    }
    return '../../../assets/videos/background_landscape_' + size + '.mp4';
  }
}

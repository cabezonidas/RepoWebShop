import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-video',
  templateUrl: './video.component.html',
  styleUrls: ['./video.component.scss']
})
export class VideoComponent implements OnInit {

  videoHeight: number;
  @ViewChild('video') public videoElement: ElementRef;
  constructor() { }

  ngOnInit() {
    this.videoHeight = this.videoElement.nativeElement.offsetHeight;
  }
  onResize(event) {
    this.videoHeight = this.videoElement.nativeElement.offsetHeight;
  }

}

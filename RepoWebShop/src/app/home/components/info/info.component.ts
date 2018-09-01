import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.scss']
})
export class InfoComponent implements OnInit {

  breakpoint = 1;
  constructor() { }

  ngOnInit() {
    this.breakpoint = (window.innerWidth <= 750) ? 1 : 2;
  }
  onResize(event) {
    this.breakpoint = (event.target.innerWidth <= 750) ? 1 : 2;
  }

}

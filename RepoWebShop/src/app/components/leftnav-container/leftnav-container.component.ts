import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-leftnav-container',
  templateUrl: './leftnav-container.component.html',
  styleUrls: ['./leftnav-container.component.scss']
})
export class LeftnavContainerComponent implements OnInit {

  @Output() close = new EventEmitter();
  constructor() { }

  ngOnInit() {
  }

}

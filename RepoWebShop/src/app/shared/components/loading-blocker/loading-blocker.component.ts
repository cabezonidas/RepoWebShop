import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-loading-blocker',
  templateUrl: './loading-blocker.component.html',
  styleUrls: ['./loading-blocker.component.scss']
})
export class LoadingBlockerComponent implements OnInit {

  @Input() saving: boolean;
  constructor() { }

  ngOnInit() {
  }

}

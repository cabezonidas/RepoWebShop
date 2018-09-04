import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-total',
  templateUrl: './total.component.html',
  styleUrls: ['./total.component.scss']
})
export class TotalComponent implements OnInit {

  @Input() price: number;
  @Input() priceInStore: number;
  constructor() { }

  ngOnInit() {
  }

}

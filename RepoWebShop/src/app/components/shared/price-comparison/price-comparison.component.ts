import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-price-comparison',
  templateUrl: './price-comparison.component.html',
  styleUrls: ['./price-comparison.component.scss']
})
export class PriceComparisonComponent implements OnInit {

  @Input() price: number;
  @Input() priceInStore: number;
  constructor() { }

  ngOnInit() {
  }

}
